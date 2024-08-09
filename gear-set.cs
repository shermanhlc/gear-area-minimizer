public class GearSet 
{
    // Gear objects that represent each gear in the GearSet
    // stores the relevant data for individual gears
    public Gear gear_1 { get; set; }
    public Gear gear_2 { get; set; }
    
    // must be provided by user, range of [-1, 1]
    // standard: 0.25, and 0.5
    public double profile_shift { get; set; }
    
    // profile shift for gear_1 and gear_2 with correct polarity
    public double profile_shift_a { get; set; }
    public double profile_shift_b { get; set; }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="gear_1">first gear, in context with program should be gear_a or gear_c</param>
    /// <param name="gear_2">second gear, in context with program should be gear_b or gear_d</param>
    /// <param name="profile_shift">absolute value of the profile shift shared by the gears</param>
    public GearSet(Gear gear_1, Gear gear_2, double profile_shift)
    {
        this.gear_1 = gear_1;
        this.gear_2 = gear_2;
        this.profile_shift = profile_shift;

        determineProfileShiftPolarity();
    }

    /// <summary>
    /// copy constructor
    /// </summary>
    /// <param name="gear_set">GearSet to copy</param>
    public GearSet(GearSet gear_set)
    {
        this.gear_1 = new Gear(gear_set.gear_1);
        this.gear_2 = new Gear(gear_set.gear_2);
        this.profile_shift = gear_set.profile_shift;

        determineProfileShiftPolarity();
    }

    /// <summary>
    /// set the polarity of the profile shift for each gear, the first gear is positive, the second is negative
    /// </summary>
    public void determineProfileShiftPolarity()
    {  
        gear_1.profile_shift = profile_shift * 1;
        gear_2.profile_shift = profile_shift * -1;
    }
}