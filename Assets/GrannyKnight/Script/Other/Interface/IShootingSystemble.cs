
namespace Refactor
{
    public interface IShootingSystemble
    {
        public void AttackOne(TestWeapon testWeapon);
        public void StopShoot();
        public virtual void AttackTwo(TestWeapon testWeapon) { }
        public virtual void Block(bool isActive) { }
    }
}