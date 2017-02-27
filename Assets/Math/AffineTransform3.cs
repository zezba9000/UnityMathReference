using System.Runtime.InteropServices;

namespace Reign
{
	[StructLayout(LayoutKind.Sequential)]
	public struct AffineTransform3
	{
		#region Properties
		public Mat3 Transform;
		public Vec3 Translation;
		#endregion

		#region Constructors
		public AffineTransform3(Mat3 transform, Vec3 translation)
        {
            Transform = transform;
            Translation = translation;
        }

		public AffineTransform3(Vec3 translation)
        {
            Transform = Mat3.identity;
            Translation = translation;
        }

		public AffineTransform3(Quat orientation, Vec3 translation)
        {
            Transform = Mat3.FromQuaternion(orientation);
            Translation = translation;
        }

		public AffineTransform3(Quat orientation, Vec3 scale, Vec3 translation)
        {
			Transform = Mat3.FromQuaternion(orientation) * scale;
            Translation = translation;
        }

		public static AffineTransform3 FromRigidTransform(RigidTransform3 transform)
        {
			AffineTransform3 result;
            result.Translation = transform.position;
            result.Transform = Mat3.FromQuaternion(transform.rotation);
			return result;
        }

		public static readonly AffineTransform3 identity = new AffineTransform3(Mat3.identity, new Vec3());
		#endregion

		#region Methods
		public AffineTransform3 Invert()
        {
			AffineTransform3 result;
			result.Transform =  Transform.Invert();
			result.Translation = Translation.Transform(result.Transform);
			result.Translation = -result.Translation;
			return result;
        }

		public AffineTransform3 Multiply(RigidTransform3 transform)
        {
			AffineTransform3 result;
            result.Transform = Mat3.FromQuaternion(transform.rotation);
            result.Transform = result.Transform.Multiply(Transform);
			result.Translation = transform.position.Transform(Transform);
			result.Translation += Translation;
			return result;
        }
		#endregion
	}
}