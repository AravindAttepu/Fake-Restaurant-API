    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace FakeRestuarantAPI.Models;

    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantID{get;set;}
        
        public string RestaurantName{get; set;}
        
        public string Address{get;set;}
    
        public string Type{get;set;}

        public bool ParkingLot{get;set;}

        


    }
