namespace DungeonSlasher
{
    /// <summary>
    /// Interface for object loaders.
    /// </summary>
    public interface IObjectLoader
    {
        /// <summary>
        /// Load a resource from a path.
        /// </summary>
        /// <param name="resourcePath">Path to the resource.</param>
        void Load(string resourcePath);
    }
}
