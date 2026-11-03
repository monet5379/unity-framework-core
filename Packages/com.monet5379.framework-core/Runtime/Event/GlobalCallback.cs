namespace Monet5379
{
    public delegate void GlobalCallback();

    public delegate void GlobalCallback<T>(T arg1);

    public delegate void GlobalCallback<T, U>(T arg1, U arg2);

    public delegate void GlobalCallback<T, U, V>(T arg1, U arg2, V arg3);

    public delegate void GlobalCallback<T, U, V, W>(T arg1, U arg2, V arg3, W arg4);
}
