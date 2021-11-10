using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Extensions;
using UnityEngine;

namespace Behaviours.Samplers
{
    public class PathSampler : MonoBehaviour
    {
        public int capacity;
        public float minimumDistance;
        public Transform target;
        public bool drawGizmos;
        
        public ImmutableList<Vector3> Positions => this._queue.ToImmutableList();

        private Queue<Vector3> _queue;

        private void Start()
        {
            this._queue = new Queue<Vector3>(this.capacity);
            this._queue.Enqueue(this.target.position);
        }

        private void Update()
        {
            var currentPosition = this.target.position;
            
            var lastPosition = this._queue.Last();
            var distance = Vector3.Distance(currentPosition, lastPosition);

            if (distance >= this.minimumDistance)
            {
                if (this._queue.Count == this.capacity)
                {
                    this._queue.Dequeue();
                }
                
                this._queue.Enqueue(currentPosition);
            }

            if (this.drawGizmos)
            {
                var points = this._queue.ToList().Append(this.target.position);
                this.gameObject.DrawLine("pathSampler", points.ToArray(), 0.5f);
            }
        }
    }
}