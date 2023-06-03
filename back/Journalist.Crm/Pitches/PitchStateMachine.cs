using Stateless;

namespace Journalist.Crm.Domain.Pitches
{
    internal class PitchStateMachine
    {
        private StateMachine<string, PitchTrigger> _pitchStateMachine;

        public PitchStateMachine(string initialState)
        {
            _pitchStateMachine = new StateMachine<string, PitchTrigger>(initialState);

            _pitchStateMachine.Configure(PitchStates.Draft)
                .PermitReentry(PitchTrigger.Modify)
                .Permit(PitchTrigger.Cancel, PitchStates.Cancelled)
                .Permit(PitchTrigger.Validate, PitchStates.ReadyToSend);

            _pitchStateMachine.Configure(PitchStates.ReadyToSend)
                .Permit(PitchTrigger.Send, PitchStates.Sent)
                .Permit(PitchTrigger.Cancel, PitchStates.Cancelled);

            _pitchStateMachine.Configure(PitchStates.Sent)
                .Permit(PitchTrigger.Accept, PitchStates.Accepted)
                .Permit(PitchTrigger.Refuse, PitchStates.Refused)
                .Permit(PitchTrigger.Cancel, PitchStates.Cancelled);

            _pitchStateMachine.Configure(PitchStates.Accepted)
                .Permit(PitchTrigger.Cancel, PitchStates.Cancelled);
        }

        public void SetStatus(string status) => _pitchStateMachine = new StateMachine<string, PitchTrigger>(status);

        public bool CanModify()=> _pitchStateMachine.CanFire(PitchTrigger.Modify);
        public bool CanValidate() => _pitchStateMachine.CanFire(PitchTrigger.Validate);
        public bool CanSend() => _pitchStateMachine.CanFire(PitchTrigger.Send);
        public bool CanAccept() => _pitchStateMachine.CanFire(PitchTrigger.Accept);
        public bool CanRefuse() => _pitchStateMachine.CanFire(PitchTrigger.Refuse);
        public bool CanCancel() => _pitchStateMachine.CanFire(PitchTrigger.Cancel);
        public string CurrentState => _pitchStateMachine.State;
    }
}
