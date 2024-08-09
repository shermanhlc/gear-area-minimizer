    public class Gear
    {   
        // teeth per inch of diameter
        public double diametral_pitch { get; set; }
        // angle of the point at the tip of a single tooth (in degrees)
        public double pressure_angle { get; set; }
        // radius of the gear measured from the origin to the contact point of the tooth
        public double pitch_radius { get; set; }

        // should only be set when Gear is used in a GearSet by the GearSet
        public double profile_shift { get; set; }

        // industry standard defaults
        const double DEFAULT_DIAMETRAL_PITCH = 10;
        const double DEFAULT_PRESSURE_ANGLE = 25;
        const double DEFAULT_PROFILE_SHIFT = 0.25;

        /// <summary>
        /// default constructor, uses DEFAULTS
        /// </summary>
        public Gear()
        {
            this.diametral_pitch = DEFAULT_DIAMETRAL_PITCH;
            this.pressure_angle = DEFAULT_PRESSURE_ANGLE;
            this.profile_shift = DEFAULT_PROFILE_SHIFT;
        }

        public Gear(double pitch_radius)
        {
            this.pitch_radius = pitch_radius;
            this.diametral_pitch = DEFAULT_DIAMETRAL_PITCH;
            this.pressure_angle = DEFAULT_PRESSURE_ANGLE;
            this.profile_shift = DEFAULT_PROFILE_SHIFT;
        }

        /// <summary>
        /// constructor with face width and profile shift, no defaults
        /// </summary>
        /// <param name="diametral_pitch">teeth per inch of diameter</param>
        /// <param name="pressure_angle">angle of the point at the tip of a single tooth</param>
        public Gear(double diametral_pitch, double pressure_angle, double pitch_radius, double profile_shift)
        {
            this.diametral_pitch = diametral_pitch;
            this.pressure_angle = pressure_angle;
            this.pitch_radius = pitch_radius;
            this.profile_shift = profile_shift;
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="gear">deep copied Gear</param>
        public Gear(Gear gear)
        {
            this.diametral_pitch = gear.diametral_pitch;
            this.pressure_angle = gear.pressure_angle;
            this.pitch_radius = gear.pitch_radius;
            this.profile_shift = gear.profile_shift;
        }
        
        /// <summary>
        /// calculates height of a single tooth
        /// </summary>
        /// <returns>height of a single tooth</returns>
        public double wholeDepth()
        {
            double whole_depth = 2.25 / this.diametral_pitch;
            return whole_depth;
        }

        /// <summary>
        /// calculates the furthest point the tooth will touch another gear, distance from the outer radius from the root radius
        /// </summary>
        /// <returns>contact point of the tooth to another gear</returns>
        public double workingDepth()
        {
            double working_depth = 2 / this.diametral_pitch;
            return working_depth;
        }

        /// <summary>
        /// calculates the bottom of a tooth (where the tooth starts)
        /// </summary>
        /// <returns>radius of the curvature of the root of a tooth</returns>
        public double rootRadius()
        {
            double root_radius = this.pitch_radius - dedendumRadius();
            return root_radius;
        }

        /// <summary>
        /// calculates the radius created of the outermost point of a tooth
        /// </summary>
        /// <returns>the outer radius from the outermost point of a tooth</returns>
        public double outerRadius()
        {
            double outer_radius = this.pitch_radius + addendumRadius();
            return outer_radius;
        }

        /// <summary>
        /// calculates the radius of the circular portion of the gear that excludes the teeth
        /// </summary>
        /// <returns>base radius of the gear</returns>
        public double baseRadius()
        {
            double pitch_radius = this.pitch_radius * Math.Cos(this.pressure_angle);
            return pitch_radius;
        }

        /// <summary>
        /// calcualtes width of a tooth at the pitch radius
        /// </summary>
        /// <returns>width of a tooth at the pitch radius</returns>
        public double arcToothThickness()
        {
            double arc_tooth_thickness = (Math.PI / 2 * this.diametral_pitch) + (addendumRadius() - 0.5 * workingDepth()) * 2 * Math.Tan(this.pressure_angle);
            return arc_tooth_thickness;
        }

        /// <summary>
        /// calculates the radius of the curve at the base of a tooth semi-tangent to the base circle
        /// </summary>
        /// <returns>fillet radius at the base of a tooth</returns>
        public double baseFilletRadius()
        {
            double base_fillet_radius = 0.3 / (2 * this.diametral_pitch);
            return base_fillet_radius;
        }

        /// <summary>
        /// calculates the distance from the pitch radius to the top of the tooth
        /// </summary>
        /// <returns>distance from the tooth tip to pitch radius</returns>
        public double addendumRadius()
        {
            double addendum_radius = (1 + profile_shift) / this.diametral_pitch;
            return addendum_radius;
        }

        /// <summary>
        /// calculates the distance from the base of the tooth to the pitch radius
        /// </summary>
        /// <returns>distance from the tooth base to pitch radius</returns>
        public double dedendumRadius()
        {
            double dedendum_radius = workingDepth() - addendumRadius();
            return dedendum_radius;
        }
    }