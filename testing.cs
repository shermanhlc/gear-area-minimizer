// using System;

// class Testing {
//     static void Main(string[] args) {
//         Gear gear_a = new Gear(10, 25, 1);
//         Gear gear_b = new Gear(10, 25, 2.9);
//         Gear gear_c = new Gear(10, 25, 1);
//         Gear gear_d = new Gear(10, 25, 2.9);

//         GearBox gear_box = new GearBox(8.41, 0.25, 0.25, gear_a, gear_b, gear_c, gear_d);

//         // test gear pitch radius's
//         Console.WriteLine("Gear A Pitch Radius: {0}", gear_a.pitch_radius);
//         Console.WriteLine("Gear B Pitch Radius: {0}", gear_b.pitch_radius);
//         Console.WriteLine("Gear C Pitch Radius: {0}", gear_c.pitch_radius);
//         Console.WriteLine("Gear D Pitch Radius: {0}\n", gear_d.pitch_radius);

//         Console.WriteLine("Current Reduction: {0}", gear_box.currentReduction());

//         // test gearArea function
//         Console.WriteLine("Gear A Area: {0}", gear_box.gearArea(gear_a));
//         Console.WriteLine("Gear B Area: {0}", gear_box.gearArea(gear_b));
//         Console.WriteLine("Gear C Area: {0}", gear_box.gearArea(gear_c));
//         Console.WriteLine("Gear D Area: {0}\n", gear_box.gearArea(gear_d));
//         //

//         // test theta functions
//         Console.WriteLine("Theta 1: {0}", gear_box.thetaCB());
//         Console.WriteLine("Theta 1 (deg.): {0}", gear_box.thetaCB() * 180 / Math.PI);
//         Console.WriteLine("Theta 2: {0}", gear_box.thetaDB());
//         Console.WriteLine("Theta 2 (deg.): {0}\n", gear_box.thetaDB() * 180 / Math.PI);


        
//         double total = gear_box.totalArea();
//         Console.WriteLine("Total Area: {0}\n", total);

//         Gear gear_1 = new Gear(10, 25, .9, .25);
//         Gear gear_2 = new Gear(10, 25, 2.38, .25);
//         Gear gear_3 = new Gear(10, 25, .9, .25);
//         Gear gear_4 = new Gear(10, 25, 2.83, .25);

//         GearBox max_gearbox = new GearBox(8.41, 0.25, 0.25, gear_1, gear_2, gear_3, gear_4);
//         Console.WriteLine("Maximum Total Area: {0}", max_gearbox.totalArea());


//         writeOptimaltoCSV(max_gearbox, "/home/sherman/archive/repos/sae-baja/gear-teeth-calc/gear-teeth-calc/gen/");

        
//         // // RESULTS //
//         // significantly faster run times when Flush() is called outside the loop (testCSVflushOUT)
//         // int runs = 90009009; // 90 009 009

//         // Stopwatch timer = Stopwatch.StartNew();
//         // testCSV(runs);
//         // timer.Stop();
//         // long timespan = timer.ElapsedMilliseconds;
//         // Console.WriteLine("Execution Time: {0}", timespan);
        
//         // Stopwatch timer2 = Stopwatch.StartNew();
//         // testCSVflushOUT(runs);
//         // timer.Stop();
//         // long timespan2 = timer2.ElapsedMilliseconds;
//         // Console.WriteLine("Execution Time: {0}", timespan2);
//         // testCSV("/home/sherman/archive/repos/sae-baja/gear-teeth-calc/gear-teeth-calc/gen/inapp.csv");
//     }

//     public static void testCSV(int iterations)
//         {
//             using(var w = new StreamWriter("/home/sherman/archive/repos/sae-baja/gear-teeth-calc/gear-teeth-calc/gen/output.csv"))
//             {
//                 for (int i = 0; i < iterations; i++)
//                 {
//                     int first = i;
//                     double second = Math.PI * i;
//                     var line = $"{first},{second}";
//                     w.WriteLine(line);
//                     w.Flush();
//                 }
//             }
//         } 

//         public static void testCSVflushOUT(int iterations)
//         {
//             using(var w = new StreamWriter("/home/sherman/archive/repos/sae-baja/gear-teeth-calc/gear-teeth-calc/gen/output2.csv"))
//             {
//                 for (int i = 0; i < iterations; i++)
//                 {
//                     int first = i;
//                     double second = Math.PI * i;
//                     var line = $"{first},{second}";
//                     w.WriteLine(line);
//                 }
//                 w.Flush();
//             }
//         } 

//         public static void testCSV(string filepath)
//         {
//             using(var w = new StreamWriter(filepath))
//             {
//                 for (int i = 0; i < 6; i++)
//                 {
//                     int first = i;
//                     double second = Math.PI * i;
//                     var line = string.Format("{0},{1}", first, second);
//                     w.WriteLine(line);
//                     w.Flush();
//                 }
//             }
//         }

    // public static void writeOptimaltoCSV(GearBox optimal_gearbox, string filepath)
    // {
    //     // format:
    //     //              A B C D
    //     // pitch radius 0 0 0 0
    //     // total area   0 0 0 0
    //     // e.g. data    . . . .
    //     // . . . .

    //     Gear[] gears = { optimal_gearbox.gear_a, optimal_gearbox.gear_b, optimal_gearbox.gear_c, optimal_gearbox.gear_d };
    //     filepath += "optimalgearbox.csv";

    //     try
    //     {
    //         using(StreamWriter csv_writer = new StreamWriter(filepath))
    //         {
    //             // write header
    //             string header = ",pitch radius,root radius,outer radius,base radius,base fillet radius,addendum radius,";
    //             header += "dedendum radius,whole depth,working depth,arc tooth thickness";

    //             csv_writer.WriteLine(header);
                
    //             int index = 0;
    //             foreach(Gear gear in gears)
    //             {
    //                 string data_line = String.Format("Gear {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
    //                                         (char)(index + 65),
    //                                         gear.pitch_radius,
    //                                         gear.rootRadius(),
    //                                         gear.outerRadius(),
    //                                         gear.baseRadius(),
    //                                         gear.baseFilletRadius(),
    //                                         gear.addendumRadius(),
    //                                         gear.dedendumRadius(),
    //                                         gear.addendumRadius(),
    //                                         gear.dedendumRadius(),
    //                                         gear.wholeDepth(),
    //                                         gear.workingDepth(),
    //                                         gear.arcToothThickness(),
    //                                         gear.baseFilletRadius()
    //                                     );

    //                 csv_writer.WriteLine(data_line);
    //                 index++;
    //             }
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine("ERROR writing to .csv: {0}", e.Message);
    //     }
//     }   
// }
