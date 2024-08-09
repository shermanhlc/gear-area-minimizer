public class GearBox
{   
    // Gear objects that represent each gear in the GearBox
    // stores the relevant data for individual gears
    public Gear gear_a { get; set; }
    public Gear gear_b { get; set; }
    public Gear gear_c { get; set; }
    public Gear gear_d { get; set; }

    // GearSet objects that represent pairs of gears
    // stores the gears and profile shift (pos/neg)
    public GearSet gear_set_ab { get; set; }
    public GearSet gear_set_cd { get; set; }

    // goal reduction for the gearbox
    public double ideal_reduction { get; set; }

    // angles in radians of the kite formed by the origins of gear_b and gear_d, and the two intersection points of those gears
    public double theta1 { get; set; }
    public double theta2 { get; set; }

    /// <summary>
    /// default constructor for GearBox
    /// </summary>
    /// <param name="ideal_reduction">the desired reduction for the gearbox</param>
    /// <param name="profile_shift">shared profile shift for both gearsets</param>
    public GearBox(double ideal_reduction, double profile_shift)
    {
        this.gear_a = new Gear();
        this.gear_b = new Gear();
        this.gear_c = new Gear();
        this.gear_d = new Gear();

        this.gear_set_ab = new GearSet(gear_a, gear_b, profile_shift);
        this.gear_set_cd = new GearSet(gear_c, gear_d, profile_shift);

        this.ideal_reduction = ideal_reduction;

        this.theta1 = thetaCB();
        this.theta2 = thetaDB();

    }

    /// <summary>
    /// constructor for GearBox that takes the ideal reduction, two profile shifts, and each gear individually as a Gear object
    /// </summary>
    /// <param name="ideal_reduction">the desired reduction for the gearbox</param>
    /// <param name="profile_shift_ab">profile shift of gears A and B</param>
    /// <param name="profile_shift_cd">profile shift of gears C and D</param>
    /// <param name="gear_a">Gear object A</param>
    /// <param name="gear_b">Gear object B</param>
    /// <param name="gear_c">Gear object C</param>
    /// <param name="gear_d">Gear object D</param>
    public GearBox(double ideal_reduction, double profile_shift_ab, double profile_shift_cd, Gear gear_a, Gear gear_b, Gear gear_c, Gear gear_d)
    {
        this.gear_a = gear_a;
        this.gear_b = gear_b;
        this.gear_c = gear_c;
        this.gear_d = gear_d;

        this.gear_set_ab = new GearSet(gear_a, gear_b, profile_shift_ab);
        this.gear_set_cd = new GearSet(gear_c, gear_d, profile_shift_cd);

        this.ideal_reduction = ideal_reduction;

        this.theta1 = thetaCB();
        this.theta2 = thetaDB();
    }

    /// <summary>
    /// constructor for GearBox that takes the ideal reduction, two profile shifts, and the gears as an array
    /// </summary>
    /// <param name="ideal_reduction">the desired reduction for the gearbox</param>
    /// <param name="profile_shift_ab">profile shift of gears A and B</param>
    /// <param name="profile_shift_cd">profile shift of gears C and D</param>
    /// <param name="gears">array of Gears</param>
    public GearBox(double ideal_reduction, double profile_shift_ab, double profile_shift_cd, Gear[] gears)
    {
        this.gear_a = gears[0];
        this.gear_b = gears[1];
        this.gear_c = gears[2];
        this.gear_d = gears[3];

        this.gear_set_ab = new GearSet(gear_a, gear_b, profile_shift_ab);
        this.gear_set_cd = new GearSet(gear_c, gear_d, profile_shift_cd);

        this.ideal_reduction = ideal_reduction;

        this.theta1 = thetaCB();
        this.theta2 = thetaDB();
    }

    /// <summary>
    /// copy constructor
    /// </summary>
    /// <param name="gear_box">GearBox to copy</param>
    public GearBox(GearBox gear_box)
    {
        this.gear_a = new Gear(gear_box.gear_a);
        this.gear_b = new Gear(gear_box.gear_b);
        this.gear_c = new Gear(gear_box.gear_c);
        this.gear_d = new Gear(gear_box.gear_d);

        this.gear_set_ab = new GearSet(gear_box.gear_set_ab);
        this.gear_set_cd = new GearSet(gear_box.gear_set_cd);

        this.ideal_reduction = gear_box.ideal_reduction;

        this.theta1 = gear_box.theta1;
        this.theta2 = gear_box.theta2;
    }

    /// <summary>
    /// finds the area of gear
    /// </summary>
    /// <param name="gear">gear object with a pitch_radius</param>
    /// <returns>returns the area of the gear</returns>
    public double gearArea(Gear gear)
    {
        double gear_area = Math.PI * Math.Pow(gear.pitch_radius, 2);
        return gear_area;
    }

    /// <summary>
    /// finds the distance between the two intersection points of gear_b and gear_d
    /// uses the formula to find the diagonal of a kite
    /// </summary>
    /// <returns>distance between the intersect points</returns>
    public double intersectDistance()
    {
        double intersect_distance = gear_b.pitch_radius * Math.Sin(this.theta1 / 2);
        return intersect_distance;
    }

    /// <summary>
    /// calculates the area of the quadrilateral formed from:
    /// origin (center point) of gear_b, origin of gear_d, the two intersection points from gear_b and gear_d
    /// </summary>
    /// <returns>area of the quadrilateral</returns>
    public double quadrilateralArea()
    {
        double quadrilateral_area = (gear_b.pitch_radius + gear_c.pitch_radius) * intersectDistance();
        return quadrilateral_area;
    }

    /// <summary>
    /// calculates the angle from the origin of gear_c (same as gear_b) and the two intersection points from gear_b and gear_d
    /// </summary>
    /// <returns>angle in degrees</returns>
    public double thetaCB()
    {
        double numerator = Math.Pow(gear_b.pitch_radius, 2) - Math.Pow(gear_d.pitch_radius, 2) - Math.Pow(gear_c.pitch_radius + gear_d.pitch_radius, 2);
        double denominator = -2 * gear_d.pitch_radius * (gear_c.pitch_radius + gear_d.pitch_radius);
        double theta = Math.Acos(numerator / denominator) * 2;
        return theta;
    }

    /// <summary>
    /// calculates the angle from the origin of gear_d and the two intersection points from gear_b and gear_d
    /// </summary>
    /// <returns>angle in degrees</returns>
    public double thetaDB()
    {
        double numerator = Math.Pow(gear_d.pitch_radius, 2) - Math.Pow(gear_b.pitch_radius, 2) - Math.Pow(gear_c.pitch_radius + gear_d.pitch_radius, 2);
        double denominator = -2 * gear_b.pitch_radius * (gear_c.pitch_radius + gear_d.pitch_radius);

        double theta = Math.Acos(numerator / denominator) * 2;
        return theta;
    }

    /// <summary>
    /// calculates the area of the segment bound by lines from the origin of gear_b to the intersection and
    /// the length of curve on the circumfernce of gear_b bound by those two points
    /// </summary>
    /// <returns>the area of the segment</returns>
    public double segment1Area()
    {
        double segment1_area = Math.Pow(gear_b.pitch_radius, 2) / 2 - (this.theta1 - Math.Sin(theta1));
        return segment1_area;
    }

    /// <summary>
    /// calculates the area of the segment bound by lines from the origin of gear_d to the intersection and
    /// the length of curve on the circumfernce of gear_d bound by those two points
    /// </summary>
    /// <returns>the area of the segment</returns>
    public double segment2Area()
    {
        double segment2_area = Math.Pow(gear_d.pitch_radius, 2) / 2 - (this.theta2 - Math.Sin(theta2));
        return segment2_area;
    }

    /// <summary>
    /// calculates the area of the overlap between gear_b and gear_d
    /// </summary>
    /// <returns>area of the overlap between gear_b and gear_d</returns>
    public double overlap()
    {
        double overlap = quadrilateralArea() - segment1Area() + segment2Area(); 
        return overlap;
    }

    /// <summary>
    /// finds the total area of gear_a, gear_b, and gear_d
    /// doesn't count gear_c, as it is entirely overlapped by gear_b
    /// subtracts the overlap between gear_b and gear_d
    /// </summary>
    /// <returns>returns the total area with no overlap</returns>
    public double totalArea()
    {
        double total_area = gearArea(gear_a) + gearArea(gear_b) + gearArea(gear_d) - overlap();
        return total_area;
    }
    
    /// <summary>
    /// calculates the redcution of the current gear setup
    /// </summary>
    /// <returns>gear reduciton of the gearbox</returns>
    public double currentReduction()
    {
        double current_reduction = (gear_b.pitch_radius / gear_a.pitch_radius) * (gear_d.pitch_radius / gear_c.pitch_radius);
        return current_reduction;
    }

    /// <summary>
    /// compares the reduction of the current gear setup to the ideal reduction within a range of the tolerance parameter
    /// </summary>
    /// <param name="tolerance"></param>
    /// <returns>true if reduction is within tolerance, otherwise false</returns>
    public bool checkReduction(double tolerance)
    {
        double range = Math.Abs(ideal_reduction - currentReduction());

        if (range <= tolerance)
            return true;
        else
            return false;
    }
    
    /// <summary>
    /// prints the pitch radius of each gear in the gearbox
    /// </summary>
    public void print()
    {
        Console.WriteLine("Gear A Pitch Radius: {0}", gear_a.pitch_radius);
        Console.WriteLine("Gear B Pitch Radius: {0}", gear_b.pitch_radius);
        Console.WriteLine("Gear C Pitch Radius: {0}", gear_c.pitch_radius);
        Console.WriteLine("Gear D Pitch Radius: {0}\n", gear_d.pitch_radius);
    }
}