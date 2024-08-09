
using System.Diagnostics;

public class Runner
{
    const double DEFAULT_PRESSURE_ANGLE = 25;
    const double DEFAULT_DIAMETRAL_PITCH = 10;

    // this should be changed only in the event that larger or smaller gears CAN be made
    // OR
    // to expedite the process of finding the optimal gear sizes, which may limit the optimal solution
    const double MAX_GEAR_SIZE = 3.5;
    const double MIN_GEAR_SIZE = 0.9;
    // this would need to be recalculated if maximum gear radius exceeds 3.5 inches
    const double MAX_TOTAL_AREA = 462;

    public static void Main()
    {
        Runner runner = new Runner();
        runner.runDefaults(8.41);

        
        // creates default gears
        Gear gear_a = new Gear(MIN_GEAR_SIZE);
        Gear gear_b = new Gear(MIN_GEAR_SIZE);
        Gear gear_c = new Gear(MIN_GEAR_SIZE);
        Gear gear_d = new Gear(MIN_GEAR_SIZE);
        Gear[] gears = new Gear[] { gear_a, gear_b, gear_c, gear_d };
        // runCustom(double ideal_reduction, minimum_pitch_radius, maximum_pitch_radius, 0.01, 0.08, Gear[] gears)
    }

    public void runDefaults(double ideal_reduction)
    {   
        // creates default gears
        Gear gear_a = new Gear(MIN_GEAR_SIZE);
        Gear gear_b = new Gear(MIN_GEAR_SIZE);
        Gear gear_c = new Gear(MIN_GEAR_SIZE);
        Gear gear_d = new Gear(MIN_GEAR_SIZE);
        Gear[] gears = new Gear[] { gear_a, gear_b, gear_c, gear_d };

        Optimizer optimizer = new Optimizer();
        GearBox gb = optimizer.defaultOptimize(ideal_reduction, MIN_GEAR_SIZE, MAX_GEAR_SIZE, gears);

        long counter = optimizer.counter;
    }

    // // needs gears made prior
    // public void runCustom(double ideal_reduction, minimum_pitch_radius, maximum_pitch_radius, 
    //                       increment, tolerance, Gear[] gears)
    // {
    //     Optimizer optimizer = new Optimizer();
    //     GearBox gb = optimizer.optimize(ideal_reduction, minimum_pitch_radius, maximum_pitch_radius, increment, tolerance, gears);

    //     optimizer.print();
    // }
}