namespace Core.Delegates
{
    public delegate void ActionRef<T1>(ref T1 args1);
    public delegate void ActionRef<T1, T2>(ref T1 args1, ref T2 args2);
}