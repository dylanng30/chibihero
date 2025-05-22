using UnityEngine;

public abstract class LowEnemy : Enemy
{
    private EIdleState idleState;
    private ERunState runState;
    private ENormalATKState normalATKState;

    protected void InitState()
    {
        /*idleState = new EIdleState(this);
        runState = new ERunState(this);
        normalATKState = new ENormalATKState(this);*/
        this.GetStateManager().ChangeState(runState);
    }
    public abstract void NormalATK();
    public void ResetHealth()
    {
        this._currentHealth = this._maxHealth;
    }

    public EIdleState GetIdleState()
    {
        return idleState;
    }
    public ERunState GetRunState()
    {
        return runState;
    }
    public ENormalATKState GetNormalATKState()
    {
        return normalATKState;
    }

}
