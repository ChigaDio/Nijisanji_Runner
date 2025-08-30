using GameCore.States.Control;
using UnityEngine;

namespace GameCore.Character.Anim
{
    public class CharacterAnimation : MonoBehaviour
    {
        /// <summary>
        /// アニメーションコントローラー(Character)
        /// </summary>
        private GameAnimatorController<CharacterAnimState, CharacterAnimStateEnum> m_animation_controller = new GameAnimatorController<CharacterAnimState, CharacterAnimStateEnum>();

        private TitleGameStateControl m_title_game_state = new TitleGameStateControl();
        /// <summary>
        /// 表情コントローラー
        /// </summary>
        private BlendSkinController m_blend_skin_controller = new BlendSkinController();

        private void Awake()
        {
            m_blend_skin_controller.SetUp(gameObject);
            m_animation_controller?.SetUp(gameObject);
            m_title_game_state.StartState();


        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_blend_skin_controller.StartBlinkLoop(max_waitDuration: 5.0f, min_waitDuratio: 2.0f);


        }

        // Update is called once per frame
        void Update()
        {
            m_title_game_state.UpdateState();
        }

        private void OnDestroy()
        {
            
        }
    }
}
