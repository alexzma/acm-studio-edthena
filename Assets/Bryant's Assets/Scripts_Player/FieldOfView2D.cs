using System.Collections.Generic;
using UnityEngine;

public class FieldOfView2D : MonoBehaviour
{
    public float viewAngle;
    public float viewRadius;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public LayerMask levelMask;

    public MeshFilter viewMeshFilter;
    public GameObject player;

    int stepCount;
    float stepAngleSize;
    Vector3 mousePos;
    
    Mesh viewMesh;
    SpriteControllerBryant sc;

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "Light";
        viewMeshFilter.mesh = viewMesh;
        sc = player.GetComponent <SpriteControllerBryant>();
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        stepCount = Mathf.RoundToInt(viewAngle * meshResolution);       // How many pieces to break viewmesh into
        stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.rotation.z - viewAngle / 2 + stepAngleSize * i;  // Divide by 2 bc start is player facing forward
            ViewCastInfo newViewCast = ViewCast(angle);
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];   // These calculations from FOV structure
                                                            // List of ints representing points of triangles, sets of 3                           

        vertices[0] = Vector3.zero;         // Instead of transform.position to be local space
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);   // Transform to local space

            if (i < vertexCount - 2)
            {
                // These calculations also from FOV structure
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        // Giving data to viewMesh to render
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    // Finding an object corner
    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, levelMask);
        if (hit)
        {
            if (ReferenceEquals(hit.collider.gameObject, player) && !sc.invincible)
            {
                player.GetComponent<SpriteControllerBryant>().takeDamage();
            }
            // Hit level object, return reference to hit point
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        } else
        {
            // No hit, return reference to end of view radius
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    // Raycast from Player to level objects
    public struct ViewCastInfo
    {
        public bool hit;        // Ray hit something?
        public Vector3 point;   // Where ray hit
        public float dst;       // Distance to hit
        public float angle;     // Angle of hit

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    // An edge object
    // I think this is really refering to the corner of an object, or edge of viewMesh
    public struct EdgeInfo
    {
        public Vector3 pointA;  // Closest point to edge on obstacle
        public Vector3 pointB;  // Closest point to edge off obstacle (or vice versa)

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
