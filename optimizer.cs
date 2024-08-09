public class Optimizer
{
    // default values for the optimizer
    const double DEFAULT_PROFILE_SHIFT = 0.25;
    const double DEFAULT_INCREMENT = 0.02;
    const double DEFAULT_TOLERANCE = 0.08;

    // this would need to be recalculated if maximum gear radius exceeds 3.5 inches
    const double MAX_TOTAL_AREA = 462;

    public long counter { get; set; }
    public GearBox? optimized_gearbox; // nullable

    /// <summary>
    /// default constructor
    /// </summary>
    public Optimizer()
    {
        counter = 0;
    }

    /// <summary>
    /// finds the optimal gear sizes for a given reduction to reduce total area of the GearBox using default increment and tolerance
    /// </summary>
    /// <param name="ideal_reduction">the desired reduction for the gearbox</param>
    /// <param name="minimum_pitch_radius">minimum manufacturable radius of a gear</param>
    /// <param name="maximum_pitch_radius">maximum manufacturable radius of a gear</param>
    /// <param name="gears">array of Gear objects (should be set to MINIMUM_PITCH_RADIUS)</param>
    /// <returns></returns>
    public GearBox defaultOptimize(double ideal_reduction, double minimum_pitch_radius, double maximum_pitch_radius, Gear[] gears)
    {   
        //string filepath = "/home/sherman/archive/repos/sae-baja/gear-teeth-calc/gear-teeth-calc/gen/";
        string filepath = "/Users/sherman/archive/repos/sae-baja/gear-teeth-calc/gear-teeth-calc/gen/";
        return optimize(filepath, minimum_pitch_radius, maximum_pitch_radius, DEFAULT_INCREMENT, DEFAULT_TOLERANCE, ideal_reduction, gears);
    }

    /// <summary>
    /// finds the optimal gear sizes for a given reduction to reduce total area of the GearBox
    /// </summary>
    /// <param name="minimum_pitch_radius">minimum manufacturable radius of a gear</param>
    /// <param name="maximum_pitch_radius">maximum manufacturable radius of a gear</param>
    /// <param name="increment">step size, determines accuracy</param>
    /// <param name="tolerance">range in which the calculated reduction is off from the ideal reduction</param>
    /// <param name="ideal_reduction">the desired reduction for the gearbox</param>
    /// <param name="gears">array of Gear objects (should be set to MINIMUM_PITCH_RADIUS)</param>
    /// <returns>optimal gearbox with smallest surface area with reduction within tolerance of the ideal reduction</returns>
    public GearBox optimize(string filepath, double minimum_pitch_radius, double maximum_pitch_radius, double increment, double tolerance, double ideal_reduction, Gear[] gears)
    {
        // start timer for execution time
        // this should be moved out of this function 
        // the timer SHOULD be started in the main function, right before execution
        Timer.start();
        
        string output_filepath = filepath + "output.csv";
        using(StreamWriter csv_writer = new StreamWriter(output_filepath))
        {
            GearBox optimal_gearbox = new GearBox(ideal_reduction, DEFAULT_PROFILE_SHIFT);
            double optimal_total_area = MAX_TOTAL_AREA;

            GearBox current_gearbox = new GearBox(ideal_reduction, DEFAULT_PROFILE_SHIFT, DEFAULT_PROFILE_SHIFT, gears);
            double current_total_area = current_gearbox.totalArea();

            for(double a = minimum_pitch_radius; a <= maximum_pitch_radius; a += increment)
            {
                for(double b = minimum_pitch_radius; b <= maximum_pitch_radius; b += increment)
                {
                    for(double c = minimum_pitch_radius; c <= maximum_pitch_radius; c += increment)
                    {
                        for(double d = minimum_pitch_radius; d <= maximum_pitch_radius; d += increment)
                        {
                            if (current_gearbox.checkReduction(tolerance))
                            {
                                current_total_area = current_gearbox.totalArea();
                                if (current_total_area < optimal_total_area)
                                {
                                    optimal_total_area = current_total_area;
                                    optimal_gearbox = new GearBox(current_gearbox);

                                    // write to csv for rotating mass comparison
                                    // append name of the file (i.e. output.csv) to the end of filepath                                    
                                }
                            }
                            // increment gear_d.pitch_radius and counter
                            current_gearbox.gear_d.pitch_radius = d;
                            counter++;                        
                        }
                        // increment gear_c.pitch_radius
                        current_gearbox.gear_c.pitch_radius = c;
                    }
                    // increment gear_b.pitch_radius
                    current_gearbox.gear_b.pitch_radius = b;
                }
                // increment gear_a.pitch_radius
                current_gearbox.gear_a.pitch_radius = a;
            }

            this.optimized_gearbox = new GearBox(optimal_gearbox);

            // stop timer for execution time
            Timer.stop();
            Timer.printLongFormTime();
        }

        // save the optimal gearbox to a csv file, then return
        writeOptimaltoCSV(this.optimized_gearbox, filepath);
        print();
        return optimized_gearbox;
    }

    /// <summary>
    /// writes the optimal gearbox to a csv file
    /// </summary>
    /// <param name="optimal_gearbox">gearbox found by optimize()</param>
    /// <param name="filepath">path to the directory to save the file in</param>
    /// <note>filepath should be the path to the directory, not the file's name</note>
    /// <note>this should be in its own class</note>
    public static void writeOptimaltoCSV(GearBox optimal_gearbox, string filepath)
    {
        // format:
        //              A B C D
        // pitch radius 0 0 0 0
        // total area   0 0 0 0
        // e.g. data    . . . .
        // . . . .

        Gear[] gears = { optimal_gearbox.gear_a, optimal_gearbox.gear_b, optimal_gearbox.gear_c, optimal_gearbox.gear_d };
        filepath += "optimalgearbox.csv";

        try
        {
            using(StreamWriter csv_writer = new StreamWriter(filepath))
            {
                // write header
                string header = ",pitch radius,root radius,outer radius,base radius,base fillet radius,addendum radius,";
                header += "dedendum radius,whole depth,working depth,arc tooth thickness";

                csv_writer.WriteLine(header);
                
                int index = 0;
                foreach(Gear gear in gears)
                {
                    string data_line = String.Format("Gear {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                                            (char)(index + 65),
                                            gear.pitch_radius,
                                            gear.rootRadius(),
                                            gear.outerRadius(),
                                            gear.baseRadius(),
                                            gear.baseFilletRadius(),
                                            gear.addendumRadius(),
                                            gear.dedendumRadius(),
                                            gear.wholeDepth(),
                                            gear.workingDepth(),
                                            gear.arcToothThickness()
                                        );

                    csv_writer.WriteLine(data_line);
                    index++;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR writing to .csv: {0}", e.Message);
        }
    }

    public void print()
    {
        if (this.optimized_gearbox == null)
            Console.WriteLine("gearbox null, consider running optimize() to set it");
        else  
            this.optimized_gearbox.print();
        
        Console.WriteLine("combinations tested: {0}", this.counter);
    }    
}
