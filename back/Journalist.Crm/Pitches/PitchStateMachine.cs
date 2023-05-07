using Stateless;

namespace Journalist.Crm.Domain.Pitches
{
    internal class PitchStateMachine
    {
        private readonly StateMachine<PitchState, PitchTrigger> _pitchStateMachine;

        public PitchStateMachine(PitchState initialState)
        {
            _pitchStateMachine = new StateMachine<PitchState, PitchTrigger>(initialState);

            _pitchStateMachine.Configure(PitchState.Draft)
                .Permit(PitchTrigger.Save, PitchState.Draft)
                .Permit(PitchTrigger.Cancel, PitchState.Cancelled)
                .Permit(PitchTrigger.Validate, PitchState.ReadyToSend);

            _pitchStateMachine.Configure(PitchState.ReadyToSend)
                .Permit(PitchTrigger.Send, PitchState.Sent)
                .Permit(PitchTrigger.Cancel, PitchState.Cancelled);

            _pitchStateMachine.Configure(PitchState.Sent)
                .Permit(PitchTrigger.Accept, PitchState.Accepted)
                .Permit(PitchTrigger.Refuse, PitchState.Refused)
                .Permit(PitchTrigger.Cancel, PitchState.Cancelled);

            _pitchStateMachine.Configure(PitchState.Accepted)
                .Permit(PitchTrigger.Cancel, PitchState.Cancelled);
        }

        public void Save() => _pitchStateMachine.Fire(PitchTrigger.Save);
        public void Validate() => _pitchStateMachine.Fire(PitchTrigger.Validate);
        public void Send() => _pitchStateMachine.Fire(PitchTrigger.Send);
        public void Accept() => _pitchStateMachine.Fire(PitchTrigger.Accept);
        public void Refuse() => _pitchStateMachine.Fire(PitchTrigger.Refuse);
        public void Cancel() => _pitchStateMachine.Fire(PitchTrigger.Cancel);

        public bool CanSave()=> _pitchStateMachine.CanFire(PitchTrigger.Save);
        public bool CanValidate() => _pitchStateMachine.CanFire(PitchTrigger.Validate);
        public bool CanSend() => _pitchStateMachine.CanFire(PitchTrigger.Send);
        public bool CanAccept() => _pitchStateMachine.CanFire(PitchTrigger.Accept);
        public bool CanRefuse() => _pitchStateMachine.CanFire(PitchTrigger.Refuse);
        public bool CanCancel() => _pitchStateMachine.CanFire(PitchTrigger.Cancel);
        public PitchState CurrentState => _pitchStateMachine.State;
    }
}
