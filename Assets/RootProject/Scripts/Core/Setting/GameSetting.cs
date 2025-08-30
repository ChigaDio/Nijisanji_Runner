using UnityEngine;

namespace GameCore.Setting
{
    public class GameSetting
    {
        private static float m_time_speed = 1.0f;
        public static float DeltaTime { get { return Time.deltaTime * m_time_speed; } }
    }
}
