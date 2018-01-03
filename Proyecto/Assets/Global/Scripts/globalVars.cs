using System;

public static class globalVars
{
    private static Random random = new Random();
    public static Random Random { get { return random; } }
    public static int FPS = 60;
    public static float frameTime = 1f / FPS;
}
