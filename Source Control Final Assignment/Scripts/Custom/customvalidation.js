$(document).ready(function () {
    $.validator.unobtrusive.adapters.addBool("gradval");
    $.validator.addMethod('gradval', function (value, element) {
        console.log(value);
        console.log(element);   
        var got = new Date($("#GraduationDate").val());
        var thresold = new Date(2020, 1, 1, 0, 0, 0, 0);
        console.log(got);
        console.log(thresold);

        return got > thresold;
    },"Graduation must be after 1-1-2020");

    $("#editform").validate({
        errorClass: "text-danger",
        errorElement: "div",
        errorPlacement: function (error, e) {
            e.parents('.form-group > div').append(error);
        },
        rules: {
            Username: {
                required: true,
            },

            Email: {
                required: true,
                email: true
            },
            MobileNo: {
                required: true,
                phone: true
            },
            Fullname: {
                required: true,
                maxlength: 50
            },
            Designation: {
                required: true,
            },
            Salary: {
                required: true,
                number: true
            },
            Age: {
                required: true,
                range: [18, 120]
            },
            GraduatioDate: {
                required: true,
                gradval:true
            }

        },
        messages: {
            Username: {
                required: "Username is Required",
            },

            Email: {
                required: "Email is Required",
                email: "Enter valid Email Address"
            },
            MobileNo: {
                required: "Mobile No is Required",
                phone: "Enter valid mobile No"
            },
            Fullname: {
                required: "Fullname is Required",
                maxlength: "Don't enter more than 50 characters"
            },
            Designation: {
                required: "Designation is Required",
            },
            Salary: {
                required: "Salary is Required",
                number: "Salary must be in numbers"
            },
            Age: {
                required: "Age is Required",
                range: "Age must be in range of 18 to 120"
            },
            GraduationDate: {
                required: "Graduation Date is Required",
                gradval:"Graduation Invalid"
            }
        }
    });
})