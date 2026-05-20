namespace Refactor
{
    public class TestWeaponSystem
    {
        // этот класс отвечает за стрельбу
        // И эфекты от стрельбы
        // У него есть подклассы стерльбы рейкаст и физические
        // 

        // Здесь должен распологаться интерфейс CurrentShoot Это та стрелковая система что мы сейчас используем  

        private ShootingRaycast _shootingRaycast;
        private ShootingPhysics _shootingPhysics;
        private HitingMelee _hitingMelee;
        private WeaponControlAnimation _weaponControlAnimation;

        private TestWeapon _currentWeapon;
        private TypeShootingSystem _currentTypeShootingSystem;
        private IShootingSystemble _currentShootingSystem;


        public TestWeaponSystem(ShootingRaycast shootingRaycast, ShootingPhysics shootingPhysics, WeaponControlAnimation weaponControlAnimation, HitingMelee hitingMelee)
        {
            _hitingMelee = hitingMelee;
            _shootingRaycast = shootingRaycast;
            _shootingPhysics = shootingPhysics;
            _weaponControlAnimation = weaponControlAnimation;
        }

        public void ChangeShootSystem(TypeShootingSystem typeShootingSystem)
        {
            if (_currentTypeShootingSystem != typeShootingSystem)
            {
                _currentTypeShootingSystem = typeShootingSystem;
                ControlShootingSystem();
            }
        }

        public void SetWeapon(TestWeapon currentWeapon)
        {
            _currentWeapon = currentWeapon;
            _weaponControlAnimation.SetWeapon(currentWeapon);
        }

        public void AttackOne(bool isFire)
        {
            if (_currentShootingSystem == null) { return; }
            if (isFire)
            {
                //Debug.Log($"_currentWeapon = {_currentWeapon == null}");
                _currentShootingSystem.AttackOne(_currentWeapon);
            }
            else
            {
                _currentShootingSystem.StopShoot();
            }
        }

        public void AttackTwo(bool isFire)
        {
            if (_currentShootingSystem == null) { return; }
            if (isFire)
            {
                _currentShootingSystem.AttackTwo(_currentWeapon);
            }
            else
            {
                _currentShootingSystem.StopShoot();
            }
        }

        public void Block(bool isActive)
        {
            if (_currentShootingSystem == null) { return; }
            _currentShootingSystem.Block(isActive);
        }

        private void ControlShootingSystem()
        {
            switch (_currentTypeShootingSystem)
            {
                case TypeShootingSystem.Raycast:
                    _currentShootingSystem = _shootingRaycast;

                    break;
                case TypeShootingSystem.Physics:

                    _currentShootingSystem = _shootingPhysics;
                    break;

                case TypeShootingSystem.Melee:

                    _currentShootingSystem = _hitingMelee;
                    break;
                case TypeShootingSystem.none:
                    _currentShootingSystem.StopShoot();
                    _currentShootingSystem = null;
                    break;

            }
        }
    }

}