/*
 * Sometimes a single builder isn't enough and multiple builders are
 * required to build different facets of an object  
 */

namespace DesignPatterns.Patterns.Builder {

    public class Employee {

        // Details
        public string Department { get; set; }
        public string Position { get; set; }

        // Address
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

    }

    // Facade
    public class EmployeeBuilder {

        protected Employee Employee = new Employee();

        public static implicit operator Employee(EmployeeBuilder builder) {
            return builder.Employee;
        }

        public EmployeeJobBuilder Works => new EmployeeJobBuilder(Employee);
        public EmployeeAddressBuilder Lives => new EmployeeAddressBuilder(Employee);

    }

    public class EmployeeJobBuilder : EmployeeBuilder {

        public EmployeeJobBuilder(Employee employee) {
            Employee = employee;
        }

        public EmployeeJobBuilder In(string department) {
            Employee.Department = department;
            return this;
        }

        public EmployeeJobBuilder AsA(string position) {
            Employee.Position = position;
            return this;
        }

    }

    public class EmployeeAddressBuilder : EmployeeBuilder {

        public EmployeeAddressBuilder(Employee employee) {
            Employee = employee;
        }

        public EmployeeAddressBuilder At(string streetAddress) {
            Employee.StreetAddress = streetAddress;
            return this;
        }

        public EmployeeAddressBuilder In(string city) {
            Employee.City = city;
            return this;
        }

        public EmployeeAddressBuilder WithPostcode(string postCode) {
            Employee.PostCode = postCode;
            return this;
        }
    }
}
