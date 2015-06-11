using UnityEngine;
using System.Collections;


public class MyTerrain : MonoBehaviour
{
    public float m_terrainSizeMultiplier = 10;      // terrain size = heightmap size * multiplier
    public float m_terrainHeightMultiplier = 0.2f;  // terrain height = terrain size * multiplier
    public bool m_makeSeaLevel = true;
    public float m_seaLevelMultiplier = 0.05f;      // sea level = terrain height * multiplier
    public float m_cliffLevelMultiplier = 0.4f;     // cliff level = terrain height * multiplier
    public float m_baseMapDistance = 1000.0f;       // distance at which the low res base map will be drawn (decrease to increase performance)
    public float m_pixelMapError = 6.0f;            // A lower pixel error will draw terrain at a higher level of detail but will be slower

    public Texture2D m_cliffTexture, m_grassTexture, m_waterTexture;

    public GameObject m_treeGameObject0, m_treeGameObject1, m_treeGameObject2;
    public int m_treeSpacing = 16;              // spacing between trees
    public float m_treeDistance = 2000.0f;      // distance at which trees will no longer be draw

    public Texture2D m_detail0, m_detail1, m_detail2;   // terrain details like grass, flowers...
    public int m_detailObjectDistance = 400;            // distance at which details will no longer be drawn
    public float m_detailObjectDensity = 4.0f;          // creates more dense details within patch
    public DetailRenderMode m_detailMode;



    int m_heightmapSize;  // heightmap size
    float m_terrainSize;
    float m_terrainHeight;
    float[,] m_terrainHeights;
    float m_seaLevel;
    float m_cliffLevel;

    Terrain m_terrainObject;
    TerrainData m_terrainData;


    // Use this for initialization
    void Start()
    {
        m_terrainObject = new Terrain();
        m_terrainData = new TerrainData();


        //----------------------------------------------------------------------------------------------
        #region setting up terrain data

        //------------------------------------------------------------------
        // terrain size

        m_terrainSize = StartMenu.m_mapSize * m_terrainSizeMultiplier;
        m_terrainHeight = m_terrainSize * m_terrainHeightMultiplier;

        m_heightmapSize = StartMenu.m_mapSize;
        m_terrainData.heightmapResolution = m_heightmapSize + 1;
        
        m_terrainData.size = new Vector3(m_terrainSize, m_terrainHeight, m_terrainSize);

        //------------------------------------------------------------------
        // terrain heights

        m_terrainHeights = StartMenu.m_grayLevels;

        if (m_makeSeaLevel == true)
        {
            m_seaLevel = m_terrainHeight * m_seaLevelMultiplier;
            this.makeWater();
        }
        else
            m_seaLevel = -1;


        m_terrainData.SetHeights(0, 0, m_terrainHeights);


        m_cliffLevel = m_terrainHeight * m_cliffLevelMultiplier;

        //------------------------------------------------------------------

        this.createSplatPrototypes(m_terrainData);
        this.createTreePrototypes(m_terrainData);
        this.createDetailPrototypes(m_terrainData);

        fillAlphaMap(m_terrainData);


        #endregion  // "setting up terrain data"
        //----------------------------------------------------------------------------------------------


        m_terrainObject = Terrain.CreateTerrainGameObject(m_terrainData).GetComponent<Terrain>();
        
		m_terrainObject.heightmapPixelError = m_pixelMapError;
        m_terrainObject.basemapDistance = m_baseMapDistance;


        this.fillTreeInstances(m_terrainObject, 0, 0);
        this.fillDetailMap(m_terrainObject, 0, 0);

		GenerateGates ();
    }

	private void GenerateGates ()
	{
		var size = m_terrainData.size.x;
		var heightsTmp = m_terrainData.GetHeights(0,0, 256, 256);
		//var heights = Terrain.activeTerrain.terrainData.GetHeights(0,0, size, size);
		//StartMenu.m_grayLevels;
	}

    // Update is called once per frame
    void Update() { }



    // Additional methods
    //
    void createSplatPrototypes(TerrainData terrainData)
    {
        SplatPrototype[] splatPrototypes = new SplatPrototype[3];

        splatPrototypes[0] = new SplatPrototype();
        splatPrototypes[0].texture = m_cliffTexture;
        splatPrototypes[0].tileOffset = new Vector2(0, 0);
        splatPrototypes[0].tileSize = new Vector2(m_terrainSize, m_terrainSize);

        splatPrototypes[1] = new SplatPrototype();
        splatPrototypes[1].texture = m_grassTexture;
        splatPrototypes[1].tileOffset = new Vector2(0, 0);
        splatPrototypes[1].tileSize = new Vector2(m_terrainSize, m_terrainSize);

        splatPrototypes[2] = new SplatPrototype();
        splatPrototypes[2].texture = m_waterTexture;
        splatPrototypes[2].tileOffset = new Vector2(0, 0);
        splatPrototypes[2].tileSize = new Vector2(m_terrainSize, m_terrainSize);

        terrainData.splatPrototypes = splatPrototypes;
    }

    void createTreePrototypes(TerrainData terrainData)
    {
        TreePrototype[] treeProtoTypes = new TreePrototype[3];

        treeProtoTypes[0] = new TreePrototype();
        treeProtoTypes[0].prefab = m_treeGameObject0;

        treeProtoTypes[1] = new TreePrototype();
        treeProtoTypes[1].prefab = m_treeGameObject1;

        treeProtoTypes[2] = new TreePrototype();
        treeProtoTypes[2].prefab = m_treeGameObject2;

        terrainData.treePrototypes = treeProtoTypes;
    }

    void createDetailPrototypes(TerrainData terrainData)
    {
        Color grassHealthyColor = Color.white;
        Color grassDryColor = Color.white;

        DetailPrototype[] detailProtoTypes = new DetailPrototype[3];

        detailProtoTypes[0] = new DetailPrototype();
        detailProtoTypes[0].prototypeTexture = m_detail0;
        detailProtoTypes[0].renderMode = m_detailMode;
        detailProtoTypes[0].healthyColor = grassHealthyColor;
        detailProtoTypes[0].dryColor = grassDryColor;

        detailProtoTypes[1] = new DetailPrototype();
        detailProtoTypes[1].prototypeTexture = m_detail1;
        detailProtoTypes[1].renderMode = m_detailMode;
        detailProtoTypes[1].healthyColor = grassHealthyColor;
        detailProtoTypes[1].dryColor = grassDryColor;

        detailProtoTypes[2] = new DetailPrototype();
        detailProtoTypes[2].prototypeTexture = m_detail2;
        detailProtoTypes[2].renderMode = m_detailMode;
        detailProtoTypes[2].healthyColor = grassHealthyColor;
        detailProtoTypes[2].dryColor = grassDryColor;

        terrainData.detailPrototypes = detailProtoTypes;
    }

    void fillAlphaMap(TerrainData terrainData)
    {
        int alphaMapSize = m_heightmapSize;   // control map that controls how the splat textures will be blended

        float[,,] map = new float[alphaMapSize, alphaMapSize, 3];

        Random.seed = 0;

        for (int x = 0; x < alphaMapSize; x++)
        {
            for (int z = 0; z < alphaMapSize; z++)
            {
                float currentHeight = terrainData.GetHeight(x, z);
                
                if (currentHeight < m_seaLevel*1.1f)     // water (sea level)
                {
                    map[z, x, 0] = 0.0f;    // cliff
                    map[z, x, 1] = 0.0f;    // grass
                    map[z, x, 2] = 1.0f;    // water
                }
                else if (currentHeight > m_cliffLevel)     // cliffs
                {
                    map[z, x, 0] = 1.0f;
                    map[z, x, 1] = 0.0f;
                    map[z, x, 2] = 0.0f;
                }
                else if (currentHeight >= m_seaLevel*1.1f && currentHeight < m_cliffLevel*0.5f)     // grass
                {
                    map[z, x, 0] = 0.0f;
                    map[z, x, 1] = 1.0f;
                    map[z, x, 2] = 0.0f;
                }
                else
                {
                    // Get the normalized terrain coordinate that
                    // corresponds to the the point.
                    float normX = x * 1.0f / (alphaMapSize - 1);
                    float normZ = z * 1.0f / (alphaMapSize - 1);

                    // Get the steepness value at the normalized coordinate.
                    float angle = terrainData.GetSteepness(normX, normZ);

                    // Steepness is given as an angle, 0..90 degrees. Divide
                    // by 90 to get an alpha blending value in the range 0..1.
                    float frac = angle / 90.0f;
                    map[z, x, 0] = frac;
                    map[z, x, 1] = 1.0f - frac;
                    map[z, x, 2] = 0.0f;
                }       
            }
        }

        terrainData.alphamapResolution = alphaMapSize;
        terrainData.SetAlphamaps(0, 0, map);
    }

    void fillTreeInstances(Terrain terrain, int tileX, int tileZ)
    {
        float treeBillboardDistance = 400.0f;
        float treeCrossFadeLength = 20.0f;
        int treeMaximumFullLODCount = 400;

        // tree noise
        //
        float treeFrq = 400.0f;
        int treeSeed = 2;
        PerlinNoise treeNoise = new PerlinNoise(treeSeed);


        Random.seed = 0;

        for (int x = 0; x < m_terrainSize; x += m_treeSpacing)
        {
            for (int z = 0; z < m_terrainSize; z += m_treeSpacing)
            {

                float unit = 1.0f / (m_terrainSize - 1);

                float offsetX = Random.value * unit * m_treeSpacing;
                float offsetZ = Random.value * unit * m_treeSpacing;

                float normX = x * unit + offsetX;
                float normZ = z * unit + offsetZ;

                // Get the steepness value at the normalized coordinate.
                float angle = terrain.terrainData.GetSteepness(normX, normZ);

                // Steepness is given as an angle, 0..90 degrees. Divide
                // by 90 to get an alpha blending value in the range 0..1.
                float frac = angle / 90.0f;

                if (frac < 0.5f) //make sure tree are not on steep slopes
                {
                    float worldPosX = x + tileX * (m_terrainSize - 1);
                    float worldPosZ = z + tileZ * (m_terrainSize - 1);

                    float noise = treeNoise.FractalNoise2D(worldPosX, worldPosZ, 3, treeFrq, 1.0f);
                    float ht = terrain.terrainData.GetInterpolatedHeight(normX, normZ);

                    if (noise > 0.0f && ht > m_seaLevel*1.1f && ht < m_cliffLevel)
                    {

                        TreeInstance temp = new TreeInstance();
                        temp.position = new Vector3(normX, ht, normZ);
                        temp.prototypeIndex = Random.Range(0, 3);
                        temp.widthScale = 1;
                        temp.heightScale = 1;
                        temp.color = Color.white;
                        temp.lightmapColor = Color.white;

                        terrain.AddTreeInstance(temp);
                    }
                }
            }
        }

        terrain.treeDistance = m_treeDistance;
        terrain.treeBillboardDistance = treeBillboardDistance;
        terrain.treeCrossFadeLength = treeCrossFadeLength;
        terrain.treeMaximumFullLODCount = treeMaximumFullLODCount;
    }

    void fillDetailMap(Terrain terrain, int tileX, int tileZ)
    {
        // detail resolution
        //
        int detailMapSize = m_heightmapSize;      // resolutions of detail layers
        int detailResolutionPerPatch = 8;   // detail resolution per patch must be >= 8

        // datail noise
        //
        float detailFrq = 100.0f;
        int detailSeed = 3;
        PerlinNoise detailNoise = new PerlinNoise(detailSeed);


        //each layer is drawn separately so if you have a lot of layers your draw calls will increase 
        int[,] detailMap0 = new int[detailMapSize, detailMapSize];
        int[,] detailMap1 = new int[detailMapSize, detailMapSize];
        int[,] detailMap2 = new int[detailMapSize, detailMapSize];

        float ratio = (float)m_terrainSize / (float)detailMapSize;

        Random.seed = 0;

        for (int x = 0; x < detailMapSize; x++)
        {
            for (int z = 0; z < detailMapSize; z++)
            {
                detailMap0[z, x] = 0;
                detailMap1[z, x] = 0;
                detailMap2[z, x] = 0;

                float unit = 1.0f / (detailMapSize - 1);

                float normX = x * unit;
                float normZ = z * unit;

                // Get the steepness value at the normalized coordinate.
                float angle = terrain.terrainData.GetSteepness(normX, normZ);

                // Steepness is given as an angle, 0..90 degrees. Divide
                // by 90 to get an alpha blending value in the range 0..1.
                float frac = angle / 90.0f;

                if (frac < 0.5f)
                {
                    float worldPosX = (x + tileX * (detailMapSize - 1)) * ratio;
                    float worldPosZ = (z + tileZ * (detailMapSize - 1)) * ratio;

                    float noise = detailNoise.FractalNoise2D(worldPosX, worldPosZ, 3, detailFrq, 1.0f);

                    if (noise > 0.0f)
                    {
                        float rnd = Random.value;
                        //Randomly select what layer to use
                        if (rnd < 0.33f)
                            detailMap0[z, x] = 1;
                        else if (rnd < 0.66f)
                            detailMap1[z, x] = 1;
                        else
                            detailMap2[z, x] = 1;
                    }
                }
            }
        }

        terrain.terrainData.wavingGrassStrength = 0.4f;
        terrain.terrainData.wavingGrassAmount = 0.2f;
        terrain.terrainData.wavingGrassSpeed = 0.4f;
        terrain.terrainData.wavingGrassTint = Color.white;
        terrain.detailObjectDensity = m_detailObjectDistance;
        terrain.detailObjectDistance = m_detailObjectDensity;
        terrain.terrainData.SetDetailResolution(detailMapSize, detailResolutionPerPatch);

        terrain.terrainData.SetDetailLayer(0, 0, 0, detailMap0);
        terrain.terrainData.SetDetailLayer(0, 0, 1, detailMap1);
        terrain.terrainData.SetDetailLayer(0, 0, 2, detailMap2);
    }

    void makeWater()
    {
        for (int x = 0; x < m_heightmapSize; x++)
            for (int z = 0; z < m_heightmapSize; z++)
            {
                if (m_terrainHeights[x, z] * m_terrainHeight <= m_seaLevel)
                {
                    m_terrainHeights[x, z] = m_seaLevel / m_terrainHeight;
                }
            }
    }

}

