[System.Serializable]
public class DataStore
{
    // Our saved ref to the DigSites
    private DigSite[] _digSites;

    public DataStore(DigSite[] digSites)
    {
        _digSites = digSites;
    }

    // How we will get them back out once we make a datastore
    public DigSite[] getDigSites()
    {
        return _digSites;
    }
}
