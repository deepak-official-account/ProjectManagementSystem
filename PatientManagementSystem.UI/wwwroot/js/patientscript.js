function getPatientsData() {
    $(document).ready(function () {
        $.ajax({
            url: "https://localhost:7200/api/PatientApi/get-all-patients",
            method: "GET",
            success: function (data) {
                function Patient(Id, Name, Email, Age, PhoneNumber) {
                    this.Id = Id;
                    this.Name = Name;
                    this.Email = Email;
                    this.Age = Age;
                    this.PhoneNumber = PhoneNumber;
                }

                let patients = [];

                $.each(data, function (index, patient) {
                    let newPatient = new Patient(
                        patient.Id,
                        patient.Name,
                        patient.Email,
                        patient.Age,
                        patient.PhoneNumber
                    );
                    patients.push(newPatient);
                });

                console.log("Fetched patients:", patients);
                // You can use patients array here or call a callback
            },
            error: function (xhr, status, error) {
                console.error("Error fetching patient data:", error);
            }
        });
    });
}
