public interface ITarget {
    string type { get;}

    /// <summary>
    /// Returns true if object passed meets requirements
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Validate(object obj);
}
