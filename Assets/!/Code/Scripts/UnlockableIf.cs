public interface UnlockableIf {
    public bool IsLocked();

    public void Unlock();

    public void TryOpen();
}