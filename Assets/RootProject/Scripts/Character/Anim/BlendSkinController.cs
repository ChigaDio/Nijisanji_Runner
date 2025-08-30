using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;

namespace GameCore.Character.Anim
{
    /// <summary>
    /// �u�����h�R���g���[���[
    /// </summary>
    public class BlendSkinController
    {
        private SkinnedMeshRenderer _renderer;

        public class BlendSkinDetailsData
        {
            private int index = 0;
            private float weight = 0.0f;
            public static readonly float MAX_WHEIGHT = 100.0f;
            public static readonly float MIN_WHEIGHT = 0.0f;

            public BlendSkinDetailsData(int set_index)
            {
                index = set_index;
                weight = 0.0f;
            }

            //0~1�ɂ���
            public void SetWeight(float value)
            {
                var set_value = Mathf.Clamp(value, MIN_WHEIGHT, MAX_WHEIGHT) / MAX_WHEIGHT;
                weight = MAX_WHEIGHT * set_value;
            }

            public int Index => index;
            public float Weight => weight;
        }

        /// <summary>
        /// �u�����hID
        /// </summary>
        public Dictionary<BlendSkinID, BlendSkinDetailsData> m_blendID = new Dictionary<BlendSkinID, BlendSkinDetailsData>();

        private bool _isBlinkLoopActive = false;
        private bool _isMouthLoopActive = false;

        public void SetUp(GameObject target)
        {
            SetUp(target.GetComponentInChildren<SkinnedMeshRenderer>());
        }

        public void SetUp(SkinnedMeshRenderer render)
        {
            if (render == null) return;

            _renderer = render;

            List<Tuple<string, int>> blendShapeNames = new List<Tuple<string, int>>();
            Mesh mesh = render.sharedMesh;
            //�u�����h���擾
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                blendShapeNames.Add(new Tuple<string, int>(mesh.GetBlendShapeName(i), i));
            }

            foreach (var blendShapeName in blendShapeNames)
            {
                bool findBreak = false;
                foreach (var data in BlendSkinData.BlendMappings)
                {
                    if (findBreak) break;
                    foreach (var dataValue in data.Value)
                    {
                        if (dataValue == blendShapeName.Item1)
                        {
                            BlendSkinDetailsData setData = new BlendSkinDetailsData(blendShapeName.Item2);
                            m_blendID.Add(data.Key, setData);
                            findBreak = true;
                            break;
                        }
                    }
                }
            }
        }

        public void SetBlendWeight(BlendSkinID id, float weight0to100)
        {
            if (_renderer == null) return;
            if (m_blendID.TryGetValue(id, out var data))
            {
                data.SetWeight(weight0to100);
                _renderer.SetBlendShapeWeight(data.Index, data.Weight);
            }
        }

        /// <summary>
        /// �w�肵��ID�̃u�����h�V�F�C�v���A�w�肵�����ԂŎw�肵���E�F�C�g(0~1)�ɑJ�ڂ�����B
        /// �I�����Ƀ����_���Ăяo���B
        /// </summary>
        public async UniTask Transition(float duration, float targetWeight01, BlendSkinID id, Action onComplete = null)
        {
            if (_renderer == null) return;
            if (!m_blendID.TryGetValue(id, out var data)) return;

            float start = data.Weight / BlendSkinDetailsData.MAX_WHEIGHT;
            float target = Mathf.Clamp01(targetWeight01);
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (_renderer == null) return;
                elapsed += Setting.GameSetting.DeltaTime;
                float ratio = Mathf.Clamp01(elapsed / duration);
                float current = Mathf.Lerp(start, target, ratio);
                SetBlendWeight(id, current * 100f);
                await UniTask.NextFrame();
            }

            SetBlendWeight(id, target * 100f);
            onComplete?.Invoke();
        }

        /// <summary>
        /// ������ID�𓯎��Ɂi���s�Łj�w�肵�����ԂŎw�肵���E�F�C�g(0~1)�ɑJ�ڂ�����B
        /// �I�����Ƀ����_���Ăяo���B
        /// </summary>
        public async UniTask TransitionParallel(float duration, IReadOnlyDictionary<BlendSkinID, float> targets01, Action onComplete = null)
        {
            if (_renderer == null) return;

            var starts = new Dictionary<BlendSkinID, float>();
            foreach (var kv in targets01)
            {
                if (m_blendID.TryGetValue(kv.Key, out var data))
                {
                    starts[kv.Key] = data.Weight / 100f;
                }
            }

            if (starts.Count == 0) return;

            float elapsed = 0f;
            while (elapsed < duration)
            {
                if (_renderer == null) return;
                elapsed += Setting.GameSetting.DeltaTime;
                float ratio = Mathf.Clamp01(elapsed / duration);
                foreach (var kv in targets01)
                {
                    if (starts.TryGetValue(kv.Key, out var start))
                    {
                        float current = Mathf.Lerp(start, Mathf.Clamp01(kv.Value), ratio);
                        SetBlendWeight(kv.Key, current * 100f);
                    }
                }
                await UniTask.NextFrame();
            }

            foreach (var kv in targets01)
            {
                SetBlendWeight(kv.Key, Mathf.Clamp01(kv.Value) * 100f);
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// �܂΂����������i���� -> �z�[���h -> �J���̃V�[�P���X�j�B
        /// </summary>
        public async UniTask DoBlink(float closeDuration = 0.1f, float max_holdDuration = 0.1f, float min_holdDuratio = 0.05f, float max_waitDuration = 0.1f, float min_waitDuratio = 0.05f, float openDuration = 0.1f)
        {
            if (_renderer == null) return;
            await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(max_waitDuration, min_waitDuratio)));
            await Transition(closeDuration, 1f, BlendSkinID.Blink);
            await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(min_holdDuratio,max_holdDuration)));
            await Transition(openDuration, 0f, BlendSkinID.Blink);
        }

        /// <summary>
        /// ���ς��������i�J�� -> ����̃V�[�P���X�j�B�K�v�ɉ����ČJ��Ԃ��Ăяo���\�B
        /// </summary>
        public async UniTask DoMouthPak(float openDuration = 0.2f, float closeDuration = 0.2f)
        {
            if (_renderer == null) return;
            await Transition(openDuration, 1f, BlendSkinID.MouthSquare);
            await Transition(closeDuration, 0f, BlendSkinID.MouthSquare);
        }

        /// <summary>
        /// �܂΂����̃��[�v���J�n����B
        /// </summary>
        public void StartBlinkLoop(float closeDuration = 0.1f, float max_holdDuration = 0.1f, float min_holdDuratio= 0.05f, float max_waitDuration = 0.1f, float min_waitDuratio = 0.05f, float openDuration = 0.1f)
        {
            if (_isBlinkLoopActive) return; // ���ɋN�����Ȃ牽�����Ȃ�
            _isBlinkLoopActive = true;
            BlinkLoopAsync(closeDuration, max_holdDuration,min_holdDuratio, max_waitDuration,min_waitDuratio, openDuration).Forget();
        }

        /// <summary>
        /// �܂΂����̃��[�v���~����B
        /// </summary>
        public void StopBlinkLoop()
        {
            _isBlinkLoopActive = false;
        }

        private async UniTask BlinkLoopAsync(float closeDuration, float max_holdDuration,float min_holdDuratio, float max_waitDuration, float min_waitDuration, float openDuration)
        {
            while (_isBlinkLoopActive)
            {
                if (_renderer == null)
                {
                    _isBlinkLoopActive = false;
                    return;
                }
                await DoBlink(closeDuration, max_holdDuration, min_holdDuratio,max_waitDuration,min_waitDuration, openDuration);
            }

            // ���[�v�I������0.0�ɑJ��
            if (_renderer != null)
            {
                await Transition(0.1f, 0f, BlendSkinID.Blink);
            }
        }

        /// <summary>
        /// ���ς��̃��[�v���J�n����B
        /// </summary>
        public void StartMouthLoop(float openDuration = 0.2f, float closeDuration = 0.2f)
        {
            if (_isMouthLoopActive) return; // ���ɋN�����Ȃ牽�����Ȃ�
            _isMouthLoopActive = true;
            MouthLoopAsync(openDuration, closeDuration).Forget();
        }

        /// <summary>
        /// ���ς��̃��[�v���~����B
        /// </summary>
        public void StopMouthLoop()
        {
            _isMouthLoopActive = false;
        }

        private async UniTask MouthLoopAsync(float openDuration, float closeDuration)
        {
            while (_isMouthLoopActive)
            {
                if (_renderer == null)
                {
                    _isMouthLoopActive = false;
                    return;
                }
                await DoMouthPak(openDuration, closeDuration);
            }

            // ���[�v�I������0.0�ɑJ��
            if (_renderer != null)
            {
                await Transition(0.1f, 0f, BlendSkinID.MouthSquare);
            }
        }
    }
}