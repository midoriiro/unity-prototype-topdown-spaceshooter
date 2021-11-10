using Systems.Helpers;
using Systems.Inputs.Extensions;
using Systems.Inputs.Groups;
using Core.Extensions;
using UnityEngine;

// TODO : refactoring draw gizmos
namespace Behaviours.Gameplays.Inputs
{
    public class GamePadInputController : MonoBehaviour
    {
        public float range;
        public FaceButtonInputGroup faceButtons;
        public BumperInputGroup bumpers;
        public TriggerInputGroup triggers;
        public StickInputGroup sticks;
        public DigitalPadInputGroup digitalPad;

        private void Update()
        {
            this.triggers.left.Update();
            this.triggers.leftShared.Update();
            this.triggers.right.Update();
            this.triggers.rightShared.Update();
            this.sticks.left.Update();
            this.sticks.right.Update();
            this.digitalPad.axis.Update();
        }

        private void OnDrawGizmos()
        {
            this.OnFaceButtonsDrawGizmos();
            this.OnFaceButtonsMenuAndBackDrawGizmos();
            this.OnBumpersDrawGizmos();
            this.OnTriggersDrawGizmos();
            this.OnSticksDrawGizmos();
            this.OnStickButtonsDrawGizmo();
            this.OnDigitalPadDrawGizmos();
        }

        private void OnFaceButtonsDrawGizmos()
        {
            var width = this.range * 0.25f;
            var marginConstant = 2;
            var margin = marginConstant * 2 + marginConstant / 2f;
            var resolution = 20;
            var position = this.transform.position;
            
            var distance = this.range + width + width + width / 2f;
            var leftGizmoPosition = Vector3.left * distance;
            var rightGizmoPosition = Vector3.right * distance;
            
            var leftLeftStartAngle = 45 + margin;
            var leftLeftEndAngle = 67.5f - margin / 2f;
            var leftRightStartAngle = 67.5f + margin / 2f;
            var leftRightEndAngle = 90 - margin / 2f;
            var rightLeftStartAngle = -90 + margin / 2f;
            var rightLeftEndAngle = -67.5f - margin / 2f;
            var rightRightStartAngle = -67.5f + margin / 2f;
            var rightRightEndAngle = -45 - margin;

            if (this.faceButtons.X.IsHold())
            {
                Gizmos.color = this.faceButtons.X.color;
                this.gameObject.DrawArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftLeftStartAngle, 
                    leftLeftEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftLeftStartAngle, 
                    leftLeftEndAngle, 
                    width, 
                    resolution
                );
            }
            
            if (this.faceButtons.Y.IsHold())
            {
                Gizmos.color = this.faceButtons.Y.color;
                this.gameObject.DrawArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftRightStartAngle, 
                    leftRightEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftRightStartAngle, 
                    leftRightEndAngle, 
                    width, 
                    resolution
                );
            }
            
            if (this.faceButtons.A.IsHold())
            {
                Gizmos.color = this.faceButtons.A.color;
                this.gameObject.DrawArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightLeftStartAngle, 
                    rightLeftEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightLeftStartAngle, 
                    rightLeftEndAngle, 
                    width, 
                    resolution
                );
            }
            
            if (this.faceButtons.B.IsHold())
            {
                Gizmos.color = this.faceButtons.B.color;
                this.gameObject.DrawArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightRightStartAngle, 
                    rightRightEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightRightStartAngle, 
                    rightRightEndAngle, 
                    width, 
                    resolution
                );
            }
        }

        private void OnFaceButtonsMenuAndBackDrawGizmos()
        {
            var width = this.range * 0.25f;
            var marginConstant = 2;
            var margin = marginConstant * 2 + marginConstant / 2f;
            var resolution = 20;
            var position = this.transform.position;
            
            var distance = this.range + width;
            var leftGizmoPosition = Vector3.left * distance;
            var rightGizmoPosition = Vector3.right * distance;
            
            var leftStartAngle = -90 + margin / 2f;
            var leftEndAngle = -45 - margin;
            var rightStartAngle = 90 - margin / 2f;
            var rightEndAngle = 45 + margin;

            if (this.faceButtons.view.IsHold())
            {
                Gizmos.color = this.faceButtons.view.color;
                this.gameObject.DrawArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftEndAngle, 
                    width, 
                    resolution
                );
            }
            
            if (this.faceButtons.menu.IsHold())
            {
                Gizmos.color = this.faceButtons.menu.color;
                this.gameObject.DrawArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightEndAngle, 
                    width, 
                    resolution
                );
            }
        }
        
        private void OnBumpersDrawGizmos()
        {
            var width = this.range * 0.25f;
            var marginConstant = 2;
            var margin = marginConstant * 2 + marginConstant / 2f;
            var resolution = 20;
            var position = this.transform.position;
            
            var distance = this.range + width + width + width / 2f;
            var leftGizmoPosition = Vector3.left * distance;
            var rightGizmoPosition = Vector3.right * distance;
            
            var leftStartAngle = -90 + margin / 2f;
            var leftEndAngle = -45 - margin;
            var rightStartAngle = 90 - margin / 2f;
            var rightEndAngle = 45 + margin;

            if (this.bumpers.left.IsHold())
            {
                Gizmos.color = this.bumpers.left.color;
                this.gameObject.DrawArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftEndAngle, 
                    width, 
                    resolution
                );
            }
            
            if (this.bumpers.right.IsHold())
            {
                Gizmos.color = this.bumpers.right.color;
                this.gameObject.DrawArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightEndAngle, 
                    width, 
                    resolution
                );
            }
        }

        private void OnTriggersDrawGizmos()
        {
            var width = this.range * 0.25f;
            var resolution = 20;
            var position = this.transform.position;
            
            var leftDirection = this.triggers.left.Direction();
            var leftSharedDirection = this.triggers.leftShared.Direction();
            var rightDirection = this.triggers.right.Direction();
            var rightSharedDirection = this.triggers.rightShared.Direction();
            
            var leftSize = Mathf.Min(1, leftDirection.magnitude);
            var leftSharedSize = Mathf.Min(1, leftSharedDirection.magnitude);
            var rightSize = Mathf.Min(1, rightDirection.magnitude);
            var rightSharedSize = Mathf.Min(1, rightSharedDirection.magnitude);

            var distance = this.range + width;
            var sharedDistance = this.range + width * 2 + width / 2f;
            
            var leftGizmoPosition = Vector3.left * distance;
            var leftSharedGizmoPosition = Vector3.left * sharedDistance;
            var rightGizmoPosition = -leftGizmoPosition;
            var rightSharedGizmoPosition = -leftSharedGizmoPosition;

            var leftStartAngle = 45;
            var leftEndAngle = -45;
            var rightStartAngle = -45;
            var rightEndAngle = 45;
            
            var leftGizmoSize = MathHelper.Map(
                leftSize, 
                0, 
                1, 
                leftStartAngle, 
                leftEndAngle
            );
            var leftSharedGizmoSize = MathHelper.Map(
                leftSharedSize, 
                0, 
                1, 
                leftStartAngle, 
                leftEndAngle
            );
            var rightGizmoSize = MathHelper.Map(
                rightSize, 
                0, 
                1, 
                rightStartAngle, 
                rightEndAngle
            );
            var rightSharedGizmoSize = MathHelper.Map(
                rightSharedSize, 
                0, 
                1, 
                rightStartAngle, 
                rightEndAngle
            );
                
            Gizmos.color = Color.grey;
            this.gameObject.DrawWireArc(
                position, 
                leftGizmoPosition, 
                Vector3.forward, 
                leftStartAngle, 
                leftEndAngle, 
                width, 
                resolution
            );
            this.gameObject.DrawWireArc(
                position, 
                leftSharedGizmoPosition, 
                Vector3.forward, 
                leftStartAngle, 
                leftEndAngle, 
                width, 
                resolution
            );
            this.gameObject.DrawWireArc(
                position, 
                rightGizmoPosition, 
                Vector3.forward, 
                rightStartAngle, 
                rightEndAngle, 
                width, 
                resolution
            );
            this.gameObject.DrawWireArc(
                position, 
                rightSharedGizmoPosition, 
                Vector3.forward, 
                rightStartAngle, 
                rightEndAngle, 
                width, 
                resolution
            );

            if (leftSize > float.Epsilon)
            {
                Gizmos.color = this.triggers.left.color;
                this.gameObject.DrawArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftGizmoSize, 
                    width, 
                    resolution
                );   
            }

            if (leftSharedSize > float.Epsilon)
            {
                Gizmos.color = this.triggers.leftShared.color;
                this.gameObject.DrawArc(
                    position, 
                    leftSharedGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftSharedGizmoSize, 
                    width, 
                    resolution
                );   
            }

            if (rightSize > float.Epsilon)
            {
                Gizmos.color = this.triggers.right.color;
                this.gameObject.DrawArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightGizmoSize, 
                    width, 
                    resolution
                );    
            }

            if (rightSharedSize > float.Epsilon)
            {
                Gizmos.color = this.triggers.rightShared.color;
                this.gameObject.DrawArc(
                    position, 
                    rightSharedGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightSharedGizmoSize, 
                    width, 
                    resolution
                );    
            }
        }

        private void OnSticksDrawGizmos()
        {
            var position = this.transform.position;
            var leftDirection = this.sticks.left.Direction();
            var rightDirection = this.sticks.right.Direction();
            var leftSize = Mathf.Min(1, leftDirection.magnitude);
            var rightSize = Mathf.Min(1, rightDirection.magnitude);
            
            Gizmos.color = Color.grey;
            // Gizmos.DrawWireSphere(position, this.range);
            this.gameObject.DrawWireArc(
                position,
                Vector3.up * this.range,
                Vector3.forward, 
                0f,
                360f,
                0.01f
            );
            
            if (leftSize > float.Epsilon || rightSize > float.Epsilon)
            {
                Gizmos.color = Color.white;
                // Gizmos.DrawWireSphere(position, leftSize * this.range);
                this.gameObject.DrawWireArc(
                    position,
                    Vector3.up * leftSize * this.range,
                    Vector3.forward, 
                    0f,
                    360f,
                    0.01f
                );
                // Gizmos.DrawWireSphere(position, rightSize * this.range);
                this.gameObject.DrawWireArc(
                    position,
                    Vector3.up * rightSize * this.range,
                    Vector3.forward, 
                    0f,
                    360f,
                    0.01f
                );
            }
            
            Gizmos.color = this.sticks.left.horizontal.color;
            Gizmos.DrawLine(
                position, 
                position + this.sticks.left.horizontal.Direction() * this.sticks.left.horizontal.value * this.range
            );
            
            Gizmos.color = this.sticks.left.vertical.color;
            Gizmos.DrawLine(
                position, 
                position + this.sticks.left.vertical.Direction() * this.sticks.left.vertical.value * this.range
            );
            
            Gizmos.color = this.sticks.right.horizontal.color;
            Gizmos.DrawLine(
                position, 
                position + -this.sticks.right.horizontal.Direction() * this.sticks.right.horizontal.value * this.range
            );
            
            Gizmos.color = this.sticks.right.vertical.color;
            Gizmos.DrawLine(
                position, 
                position + -this.sticks.right.vertical.Direction() * this.sticks.right.vertical.value * this.range
            );
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(position, position + leftDirection.normalized * this.range);
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(position, position + rightDirection.normalized * this.range);
        }
        
         private void OnStickButtonsDrawGizmo()
        {
            var width = this.range * 0.25f;
            var marginConstant = 2;
            var margin = marginConstant * 2 + marginConstant / 2f;
            var resolution = 20;
            var position = this.transform.position;
            
            var distance = this.range + width;
            var leftGizmoPosition = Vector3.left * distance;
            var rightGizmoPosition = Vector3.right * distance;
            
            var leftStartAngle = 90 - margin / 2f;
            var leftEndAngle = 45 + margin;
            var rightStartAngle = -90 + margin / 2f;
            var rightEndAngle = -45 - margin;

            if (this.sticks.leftButton.IsHold())
            {
                Gizmos.color = this.sticks.leftButton.color;
                this.gameObject.DrawArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    leftGizmoPosition, 
                    Vector3.forward, 
                    leftStartAngle, 
                    leftEndAngle, 
                    width, 
                    resolution
                );
            }
            
            if (this.sticks.rightButton.IsHold())
            {
                Gizmos.color = this.sticks.rightButton.color;
                this.gameObject.DrawArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightEndAngle, 
                    width, 
                    resolution
                );
            }
            else
            {
                Gizmos.color = Color.grey;
                this.gameObject.DrawWireArc(
                    position, 
                    rightGizmoPosition, 
                    Vector3.forward, 
                    rightStartAngle, 
                    rightEndAngle, 
                    width, 
                    resolution
                );
            }
        }
        
        private void OnDigitalPadDrawGizmos()
        {
            var width = this.range * 0.125f;
            var marginConstant = 2;
            var margin = marginConstant * 2 + marginConstant / 2f;
            var resolution = 20;
            var position = this.transform.position;
            
            var direction = this.digitalPad.axis.Direction();
            var size = Mathf.Min(1, direction.magnitude);
            var distance = this.range + width / 2f;
            var gizmoPosition = direction.normalized * distance;

            var leftGizmoPosition = Vector3.left * distance;
            var leftUpGizmoPosition = Quaternion.AngleAxis(-45, Vector3.forward) * leftGizmoPosition;
            var upGizmoPosition = Vector3.up * distance;
            var rightUpGizmoPosition = Quaternion.AngleAxis(-45, Vector3.forward) * upGizmoPosition;
            var rightGizmoPosition = Vector3.right * distance;
            var rightDownGizmoPosition = Quaternion.AngleAxis(-45, Vector3.forward) * rightGizmoPosition;
            var downGizmoPosition = Vector3.down * distance;
            var leftDownGizmoPosition = Quaternion.AngleAxis(-45, Vector3.forward) * downGizmoPosition;

            var startAngle = -22.5f + margin / 2f;
            var endAngle = 22.5f - margin / 2f;
            
            Gizmos.color = Color.grey;
            
            this.gameObject.DrawWireArc(
                position, 
                leftGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                leftUpGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                upGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                rightUpGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                rightGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                rightDownGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                downGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle,
                width, 
                resolution
            );
            
            this.gameObject.DrawWireArc(
                position, 
                leftDownGizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
            Gizmos.color = Color.white;
            
            this.gameObject.DrawArc(
                position, 
                gizmoPosition, 
                Vector3.forward, 
                startAngle, 
                endAngle, 
                width, 
                resolution
            );
            
        }
    }
}