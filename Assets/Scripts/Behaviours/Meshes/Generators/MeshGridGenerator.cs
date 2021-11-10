using UnityEngine;

namespace Behaviours.Meshes.Generators
{
    public class MeshGridGenerator : MonoBehaviour
    {
        public Vector2Int size;
        public MeshFilter filter;
        public new MeshRenderer renderer;

        private Mesh _mesh;
        private Vector3[]_vertices;
        private int[] _triangles;

        private void Start()
        {
            this._mesh = new Mesh();
            this.filter.mesh = this._mesh;
            this.CreateShape();
            this.UpdateMesh();
        }

        private void CreateShape()
        {
            this._vertices = new Vector3[(this.size.x + 1) * (this.size.y + 1)];
            
            for (int i = 0, y = 0; y <= this.size.y; y++)
            {
                for (var x = 0; x <= this.size.x; x++)
                {
                    this._vertices[i] = new Vector3(x, y, 0);
                    i++;
                }
            }
            
            this._triangles = new int[this.size.x * this.size.y * 6];

            var vertexIndex = 0;
            var triangleIndex = 0;

            for (int y = 0; y < this.size.y; y++)
            {
                for (var x = 0; x < this.size.x; x++)
                {
                    this._triangles[triangleIndex + 0] = vertexIndex + 0;
                    this._triangles[triangleIndex + 1] = vertexIndex + this.size.x + 1;
                    this._triangles[triangleIndex + 2] = vertexIndex + 1;
                    this._triangles[triangleIndex + 3] = vertexIndex + 1;
                    this._triangles[triangleIndex + 4] = vertexIndex + this.size.x + 1;
                    this._triangles[triangleIndex + 5] = vertexIndex + this.size.x + 2;

                    vertexIndex++;
                    triangleIndex += 6;
                }

                vertexIndex++;
            }
        }

        private void UpdateMesh()
        {
            this._mesh.Clear();
            this._mesh.vertices = this._vertices;
            this._mesh.triangles = this._triangles;
            this._mesh.RecalculateNormals();
        }
    }
}