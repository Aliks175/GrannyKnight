using UnityEngine;

namespace Refactor
{


    public class TestPlayerLook
    {
        private Transform _headSlot;
        private Transform _body;
        private float _sensitivityY;
        private float _sensitivityX;
        private float _coefficientAim;
        private float _sensitivity;
        private float _xRotation = 0;
        private float _coefficientSensitivityAim;

        public TestPlayerLook(SettingsPlayer settingsPlayer, Transform headSlot, Transform body)
        {
            _headSlot = headSlot;
            _body = body;
            _sensitivityY = settingsPlayer.SensitivityY;
            _sensitivityX = settingsPlayer.SensitivityX;
            _coefficientSensitivityAim = 1;
            _sensitivity = 1;
        }


        public void ProcessLook(Vector2 vector2)
        {
            //if (!_isPlay) return;
            float mouseX = vector2.x;
            float mouseY = vector2.y;


            mouseX *= _coefficientSensitivityAim * _sensitivity;
            mouseY *= _coefficientSensitivityAim * _sensitivity;
            _xRotation -= (mouseY * Time.deltaTime) * _sensitivityY;// * _sensitivity;
            _xRotation = Mathf.Clamp(_xRotation, -70, 70);
            _headSlot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

            _body.Rotate((mouseX * Time.deltaTime) * _sensitivityX * Vector3.up); // * _sensitivity);
        }

        /// <summary>
        /// Для изменения чувствительности во время прицеливания
        /// </summary>
        /// <param name="coefficientSpeed"></param>
        public void ChangeCoefficientSensitivityForAim(float coefficientSpeed)
        {
            ChangeCoefficientSpeed(ref _coefficientSensitivityAim, coefficientSpeed);
        }

        /// <summary>
        /// Для изменения чувствительности через настройки
        /// </summary>
        /// <param name="coefficientSpeed"></param>
        public void ChangeSensitivity(float coefficientSpeed)
        {
            ChangeCoefficientSpeed(ref _sensitivity, coefficientSpeed);
        }

        private void ChangeCoefficientSpeed(ref float coefficient, float tempCoefficientSpeed)
        {
            tempCoefficientSpeed = Mathf.Abs(tempCoefficientSpeed);
            coefficient = tempCoefficientSpeed > 1 ? 1 : tempCoefficientSpeed;
        }
    }
}