using System.Runtime.InteropServices;

namespace Reign
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RigidTransform3
	{
		#region Properties
		public Quat rotation;
		public Vec3 position;
		#endregion

		#region Constructors
		public RigidTransform3(Quat orienation, Vec3 position)
        {
            rotation = orienation;
			this.position = position;
        }

		public RigidTransform3(Vec3 position)
        {
            this.position = position;
            rotation = Quat.Identity;
        }

		public RigidTransform3(Quat orienation)
        {
            position = new Vec3();
            rotation = orienation;
        }

		public static readonly RigidTransform3 Identity = new RigidTransform3(Quat.Identity, new Vec3());
		#endregion

		#region Methods
		public RigidTransform3 Invert(ref RigidTransform3 transform)
        {
			RigidTransform3 result;
			result.rotation = transform.rotation.Conjugate();
			result.position = transform.position.Transform(result.rotation);
			return result;
        }

		public RigidTransform3 Transform(RigidTransform3 transform1, RigidTransform3 transform2)
        {
			RigidTransform3 result;
			result.position = transform1.position.Transform(transform2.rotation);
			result.position += transform2.position;
			result.rotation = transform1.rotation.Concatenate(transform2.rotation);
			return result;
        }
		#endregion
	}
}