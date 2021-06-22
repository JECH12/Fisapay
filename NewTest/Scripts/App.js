var testApp = angular.module("employeeModule", []);

testApp.controller("employeeController", function ($scope, $http) {

    $http.get("/api/Employees/").then(function (response) {
        $scope.employees = response.data;
        $scope.totalSalary = 0;
        angular.forEach($scope.employees, function (e, key) {
            $scope.totalSalary += e.Salary;
            $scope.date = new Date(e.Birthday);
            e.years = yearsCalculate($scope.date);
        });
    });

    yearsCalculate = function (date) {
        var today = new Date();
        var anios = today.getFullYear() - date.getFullYear();
        var months = today.getMonth() - date.getMonth();
        var days = today.getDate() - date.getDate();

        if (months <= 0) {
            --anios;
            months = 12 + months;
        }

        if (today.getDate() < date.getDate()) {
            --months;
            days = 30 + days;
        }
        if (months == 12) {
            ++anios;
            months = 0;
        }
        
        return anios + " years, " + months + " months and " + days + " days";

    }

    $scope.deleteData = function (employeeId) {
        if (window.confirm("Are you sure?")) {
            $http.post("/api/DeleteEmployee/" + employeeId).then(function () { window.location.reload(); });
        }
            
    }

    $scope.searchData = function (searchTerm) {
        $http.get("/api/Employees/" + searchTerm).then(function (response) {
            $scope.employees = response.data;
            $scope.totalSalary = 0;
            angular.forEach($scope.employees, function (e, key) {
                $scope.totalSalary += e.Salary;
                $scope.date = new Date(e.Birthday);
                e.years = yearsCalculate($scope.date);
            });
        });
    }
});

testApp.controller("addEmployeeController", function ($scope, $http) {
    $scope.saveData = function () {
        if ($scope.insertEmployee.$valid) {
            var data = $scope.register;
            var employeeDNI = data.DNI;
            $http.get('/api/VerifyEmployee/' + employeeDNI).then(function (response) {
                if (response.data == false) {
                    $http.post('/api/AddEmployee', JSON.stringify(data)).then(function (response) {
                        if (response = 200) {
                            $scope.register = null;
                            window.location.replace('/Home/Consult');
                        }
                    });
                }
                else {
                    $scope.employeeFound = true;
                }
                
            });
            
        }
    }
});

testApp.controller("editEmployeeController", function ($scope, $http) {
    $scope.loadRecord = function (employeeId) {
        $http.get('/api/EditEmployee/' + employeeId).then(function (response) {
            $scope.register = response.data.Employee;
            $scope.register.Covid_Vaccine = $scope.register.Covid_Vaccine.toString();
            $scope.date = new Date($scope.register.Birthday);
        });
    }

    $scope.saveData = function () {
        if ($scope.editEmployee.$valid) {
            var data = $scope.register;
            data.Birthday = $scope.date;
            $http.post('/api/EditEmployee', JSON.stringify(data)).then(function (response) {
                if (response = 200) {
                    $scope.register = null;
                    window.location.replace('/Home/Consult');
                }
            });
        }
    }
});