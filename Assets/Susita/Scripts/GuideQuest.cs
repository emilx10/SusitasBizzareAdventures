using System;
using TMPro;
using UnityEngine;

public class GuideQuest : MonoBehaviour
{
    [SerializeField] private TMP_Text _guideText;
    [SerializeField] private string _askForHelpText;
    [SerializeField] private string _thankForHelpText;
    private GameManager _gameManager;
    private Transform _player;
    private float _range = 5f; // Example range value, you can adjust this
    private Action _onUpdateEvent;
    public Action OnLambCollect;
    private State _currentState;
    [SerializeField] private LambPickUp _lamb; // Ensure these are assigned in the Inspector
    [SerializeField] private GameObject _exit;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _player = _gameManager.GetPlayerHealth().transform;
        SetState(new StateFindGuide(this));
    }

    // Update is called once per frame
    void Update()
    {
        _onUpdateEvent?.Invoke();
    }

    private bool CheckIfPlayerCloseEnough()
    {
        return Vector2.Distance(_player.position, transform.position) <= _range;
    }

    private void SpawnLamb()
    {
        _lamb.gameObject.SetActive(true);
        _lamb.SetGuideQuest(this);
    }

    private void SpawnExit()
    {
        _exit.SetActive(true);
    }

    private void SetState(State newState)
    {
        _currentState = newState;
        _currentState?.OnStart();
    }

    private abstract class State
    {
        protected GuideQuest _guideQuest;
        public abstract void OnStart();
        public abstract void OnUpdate();

        public State(GuideQuest guideQuest)
        {
            _guideQuest = guideQuest;
        }
    }

    private class StateFindGuide : State
    {
        public StateFindGuide(GuideQuest guideQuest) : base(guideQuest)
        {
        }

        public override void OnStart()
        {
            _guideQuest._onUpdateEvent = OnUpdate;
        }

        public override void OnUpdate()
        {
            if (_guideQuest.CheckIfPlayerCloseEnough())
            {
                _guideQuest.SetState(new StateGuidePlayer(_guideQuest));
            }
        }
    }

    private class StateGuidePlayer : State
    {
        public StateGuidePlayer(GuideQuest guideQuest) : base(guideQuest)
        {
        }

        public override void OnStart()
        {
            _guideQuest._guideText.text = _guideQuest._askForHelpText;
            _guideQuest.SpawnLamb();
            _guideQuest._onUpdateEvent = null;
            _guideQuest.OnLambCollect = () => _guideQuest.SetState(new StateReturnLambToGuide(_guideQuest));
        }

        public override void OnUpdate()
        {
            // Guiding player logic can be added here
        }
    }

    private class StateReturnLambToGuide : State
    {
        public StateReturnLambToGuide(GuideQuest guideQuest) : base(guideQuest)
        {
        }

        public override void OnStart()
        {
            _guideQuest._onUpdateEvent = OnUpdate;
            _guideQuest._guideText.text = string.Empty;
        }

        public override void OnUpdate()
        {
            if (_guideQuest.CheckIfPlayerCloseEnough())
            {
                _guideQuest.SetState(null); // Quest completed
                _guideQuest._onUpdateEvent = null;
                _guideQuest.SpawnExit();
                _guideQuest._guideText.text = _guideQuest._thankForHelpText;
            }
        }
    }
}
