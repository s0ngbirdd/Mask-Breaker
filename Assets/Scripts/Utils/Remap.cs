public static class Remap
{
    public static float RemapValue (float value, float fromMin, float fromMax, float toMin, float toMax) {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}
