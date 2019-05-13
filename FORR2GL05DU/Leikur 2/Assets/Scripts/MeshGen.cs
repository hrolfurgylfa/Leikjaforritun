// !!!!!----- Ég gerði mest af þessu scripti ekki en ég gerði smá kóða, minn kóði byrjar þar sem það er komment sem segir: "Byrjun kóðans míns".-----!!!!!
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MeshGen : MonoBehaviour {
    // the length of segment (world space)
    public float SegmentLength = 5;
    
    // the segment resolution (number of horizontal points)
    public int SegmentResolution = 32;
    
    // the size of meshes in the pool
    public int MeshCount = 4;
    
    // the maximum number of visible meshes. Should be lower or equal than MeshCount
    public int VisibleMeshes = 4;
    
    // the prefab including MeshFilter and MeshRenderer
    public MeshFilter SegmentPrefab;
    
    // helper array to generate new segment without further allocations
    private Vector3[] _vertexArray;
    
    // the pool of free mesh filters
    private List<MeshFilter> _freeMeshFilters = new List<MeshFilter>();
    
    // the list of used segments
    private List<Segment> _usedSegments = new List<Segment>();

    // private gameObject
    
    void Awake() {
        // Create vertex array helper
        _vertexArray = new Vector3[SegmentResolution * 2];
        
        // Build triangles array. For all meshes this array always will
        // look the same, so I am generating it once 
        int iterations = _vertexArray.Length / 2 - 1;
        var triangles = new int[(_vertexArray.Length - 2) * 3];
        
        for (int i = 0; i < iterations; ++i) {
            int i2 = i * 6;
            int i3 = i * 2;
            
            triangles[i2] = i3 + 2;
            triangles[i2 + 1] = i3 + 1;
            triangles[i2 + 2] = i3 + 0;
            
            triangles[i2 + 3] = i3 + 2;
            triangles[i2 + 4] = i3 + 3;
            triangles[i2 + 5] = i3 + 1;
        }
        
        // Create colors array. For now make it all white.
        var colors = new Color32[_vertexArray.Length];
        for (int i = 0; i < colors.Length; ++i) {
            colors[i] = new Color32(0, 0, 0, 0);
        }
        
        // Create game objects (with MeshFilter) instances.
        // Assign vertices, triangles, deactivate and add to the pool.
        for (int i = 0; i < MeshCount; ++i) {
            MeshFilter filter = Instantiate(SegmentPrefab);
        
            Mesh mesh = filter.mesh;
            mesh.Clear();
            
            mesh.vertices = _vertexArray;
            mesh.triangles = triangles;
            
            filter.gameObject.SetActive(false);
            _freeMeshFilters.Add(filter);
        }
    }
    
    void Update() {
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        int currentSegment = (int) (worldCenter.x / SegmentLength);
        
        // test for invisibility
        for (int i = 0; i < _usedSegments.Count;) {
            int segmentIndex = _usedSegments[i].Index;
            if (!IsSegmentInSight(segmentIndex)) {
                EnsureSegmentNotVisible(segmentIndex);
            } else {
                // EnsureSegmentNotVisible will remove the segment from the list
                // that's why I increase the counter based on that condition
                ++i;
            }
        }
        
        // test for visibility
        for (int i = currentSegment - VisibleMeshes / 2; i < currentSegment + VisibleMeshes / 2; ++i) {
            if (IsSegmentInSight(i)) {
                EnsureSegmentVisible(i);
            }
        }
    }
    
    private void EnsureSegmentVisible(int index) {
        if (!IsSegmentVisible(index)) {
            // make visible
            int meshIndex = _freeMeshFilters.Count - 1;
            MeshFilter filter = _freeMeshFilters[meshIndex];
            _freeMeshFilters.RemoveAt(meshIndex);
            
            Mesh mesh = filter.mesh;
            GenerateSegment(index, ref mesh, filter);
            
            filter.transform.position = new Vector3(index * SegmentLength, 0, 0);

            filter.gameObject.SetActive(true);
            
            // register as segment
            var segment = new Segment();
            segment.Index = index;
            segment.MeshFilter = filter;

            // GenerateCollider(segment);
            
            _usedSegments.Add(segment);
        }
    }
    
    private void EnsureSegmentNotVisible(int index) {
        if (IsSegmentVisible(index)) {
            int listIndex = SegmentCurrentlyVisibleListIndex(index);
            Segment segment = _usedSegments[listIndex]; 
            _usedSegments.RemoveAt(listIndex);
            
            MeshFilter filter = segment.MeshFilter;
            filter.gameObject.SetActive(false);
            
            _freeMeshFilters.Add(filter);
        }
    }
    
    private bool IsSegmentVisible(int index) {
        return SegmentCurrentlyVisibleListIndex(index) != -1;
    }
    
    private int SegmentCurrentlyVisibleListIndex(int index) {
        for (int i = 0; i < _usedSegments.Count; ++i) {
            if (_usedSegments[i].Index == index) {
                return i;
            }
        }
        
        return -1;
    }
    
    // Gets the heigh of terrain at current position.
    // Modify this fuction to get different terrain configuration.
    private float GetHeight(float position) {
        return (Mathf.Sin(position) + 5.5f + Mathf.Sin(position * .5f) + 1f) / 2f;
    }
    
    // This function generates a mesh segment.
    // Index is a segment index (starting with 0).
    // Mesh is a mesh that this segment should be written to.
    public void GenerateSegment(int index, ref Mesh mesh, MeshFilter filter) {// Hérna bætti ég við filter til þess að geta komist í EdgeCollider2D og geta breytt honum svo að hann passi við colliderinn
        float startPosition = index * SegmentLength;
        float step = SegmentLength / (SegmentResolution - 1);
        
        for (int i = 0; i < SegmentResolution; ++i) {
            // get the relative x position
            float xPos = step * i;
            
            // top vertex
            float yPosTop = GetHeight(startPosition + xPos); // position passed to GetHeight() must be absolute
            _vertexArray[i * 2] = new Vector3(xPos, yPosTop, 0);
            
            // bottom vertex always at y=0
            _vertexArray[i * 2 + 1] = new Vector3(xPos, 0, 0);
        }

        // Byrjun kóðans míns (Þetta er það sem ég eyddi lang mestum tíma í en það borgaði sig algjörlega vegna þess að núna er ég með jörð sem er búin til sjálfkrafa)

        // Það sem ég er að gera hérna í stuttu máli er að taka annan hvorn punkt í _vertexArrey, setja þá í minn egin lista og gera svo edge collider á milli þeirra allra vegna þess að þetta script sem ég fann kom ekki með collier svo að ég þurfti að eyða mörgum klukkutímum í að fara yfir þetta script til þess að skilja uþb hvað væri að gerast og gera svo minn egin edgeCollider hérna.

        int lengd;// Þetta býr til lengdar breytu sem er svo notuð til þess að passa að punktalistarnir séu ekki of langir
        if (_vertexArray.Length % 2 != 0) {// Þetta keyrir ef lengdin á _vertexArrey er oddatala
            lengd = (_vertexArray.Length / 2) - 1;// Þetta deilir _vertexArreyinum í tvent vegna þess að ég er að fara að búa til lista sem er með öðrum hverjum punkti og svo mínusa ég 1 til þess að ég sé ekki með .5 í heilu tölunni minni sem verður notuð fyrir lengdina á listanum listiPunkta
        } else {// Þetta keyrir ef lengdin á _vertexArrey er slétt tala
            lengd = _vertexArray.Length / 2;// Þetta deilir _vertexArreyinum í tvent vegna þess að ég er að fara að búa til lista sem er með öðrum hverjum punkti
        }

        Vector2[] listiPunkta = new Vector2[lengd];// Hérna bý ég til lista með lengdinni lengd

        int tel2 = 0;
        for (int tel = 0; tel < _vertexArray.Length; tel += 2) {// Hérna keyri ég í gegnum aðra hverja tölu í _vertexArrey og bæti henni í listann minn
            float x = _vertexArray[tel][0];// Hérna tex ég x gildið úr Vector3 í _vertexArrey vegna þess að mér vantar Vector2
            float y = _vertexArray[tel][1];// Hérna tex ég y gildið úr Vector3 í _vertexArrey vegna þess að mér vantar Vector2
            Vector2 punktur = new Vector2(x, y);// Hérna bý ég til Vector2 úr hnitunum sem ég tók
            listiPunkta[tel2] = punktur;// Hérna bæti ég Vector2inum við listann minn
            tel2++;
        }

        EdgeCollider2D edgeCollider = filter.gameObject.GetComponent<EdgeCollider2D>();// Hérna sæki ég edge colliderinn
        edgeCollider.Reset();// Hérna endurræsi ég edge colliderinn
        edgeCollider.points = listiPunkta;// Hérna bæti ég punktalistanum mínum sem ég bjó til hér að ofan við edge colliderinn
        // Endi kóðans míns

        mesh.vertices = _vertexArray;
        
        // need to recalculate bounds, because mesh can disappear too early
        mesh.RecalculateBounds();
    }
    
    private bool IsSegmentInSight(int index) {
        Vector3 worldLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 worldRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        
        // check left and right segment side
        float x1 = index * SegmentLength;
        float x2 = x1 + SegmentLength;
        
        return x1 <= worldRight.x && x2 >= worldLeft.x;
    }
    
    private struct Segment {
        public int Index { get; set; }
        public MeshFilter MeshFilter { get; set; }
    }
}
