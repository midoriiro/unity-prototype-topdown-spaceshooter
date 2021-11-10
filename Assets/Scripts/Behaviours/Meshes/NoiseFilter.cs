using UnityEngine;

namespace Behaviours.Meshes
{
    [ExecuteInEditMode]
    public class NoiseFilter : MonoBehaviour
    {
        [Range(-1, 1)]public float baseLine = -1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float strength = 1;
        public float persistence = .5f;
        public Vector3 center;
        [Range(1,8)] public int layersCount = 1;
        public FastNoiseUnity noise;
        
        public delegate void SettingsChangedEventHandler();
        public event SettingsChangedEventHandler SettingsChanged;

        private void Awake()
        {
            this.noise.SettingsChanged += () => this.SettingsChanged?.Invoke();
            this.OnValidate();   
        }

        private void OnValidate()
        {
            this.SettingsChanged?.Invoke();
        }

        public float Evaluate(Vector3 vertex)
        {           
            var noise = 0f;
            var frequency = this.baseRoughness;
            var amplitude = 1f;

            for (int i = 0; i < this.layersCount; i++)
            {
                var vertexToEvaluate = vertex * frequency + this.center;
                var value = this.noise.fastNoise.GetNoise(vertexToEvaluate.x, vertexToEvaluate.y, vertexToEvaluate.z);
                
                noise += value * amplitude;
                frequency *= this.roughness;
                amplitude *= this.persistence;
            }

            if (noise < this.baseLine)
            {
                noise = this.baseLine;
            }
            
            noise *= this.strength;

            return noise;
        }
    }
}