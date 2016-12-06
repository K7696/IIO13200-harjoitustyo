var app = angular.module("developer", []);


app.directive('userDialog', function() {
    return {
        restrict: 'E',
        templateUrl: '../../directives/user-dialog.html',
        transclude: true,
        link: function (scope) {
            
            $("#accountBtn").click(function () {
                $("#user-modal").modal();
            });

        }
    };
});

app.directive('taskDialog', function () {
    return {
        restrict: 'E',
        templateUrl: '../../directives/task-dialog.html',
        transclude: true,
        link: function (scope) {

        }
    };
});

app.directive('storyDialog', function () {
    return {
        restrict: 'E',
        templateUrl: '../../directives/story-dialog.html',
        transclude: true,
        link: function (scope) {

        }
    };
});

app.directive('memberDialog', function () {
    return {
        restrict: 'E',
        templateUrl: '../../directives/member-dialog.html',
        transclude: true,
        link: function (scope) {

        }
    };
});

app.directive('sprintDialog', function () {
    return {
        restrict: 'E',
        templateUrl: '../../directives/sprint-dialog.html',
        transclude: true,
        link: function (scope) {

        }
    };
});

app.directive('projectDialog', function () {
  return {
    restrict: 'E',
    templateUrl: '../../directives/project-dialog.html',
    transclude: true,
    link: function (scope) {

    }
  };
});

app.controller("developerCtrl", function ($scope, $http) {
    
    // Init lists
    $scope.items = [];
    $scope.stories = [];
    $scope.projects = [];

    // Init objects
    $scope.me = {};
    $scope.myRemainingWork = 0;
    $scope.user = {};

    $scope.activeItem = {};
    $scope.newItem = false;
    $scope.editItem = false;

    $scope.activeStory = {};
    $scope.newStory = false;
    $scope.editStory = false;

    $scope.team = {};

    $scope.activeMember = {};
    $scope.newMember = false;
    $scope.editMember = false;

    $scope.activeProject = {};
    $scope.editProject = false;
    $scope.newProject = false;

    $scope.activeSprint = {};
    $scope.editSprint = false;

    // Init buttons
    $scope.viewMembersButton = false;
    $scope.hideMembersButton = false;
    $scope.viewProductBacklogButton = false;
    $scope.hideProductBacklogButton = false;
    $scope.viewProjectsButton = false;
    $scope.hideProjectsButton = false;

    // Init
    (function () {
        _getUser();
        _getItems();        
        _getTeam();

        $scope.viewMembersButton = true;
        $scope.viewProductBacklogButton = true;
        $scope.viewProjectsButton = true;
    }());

    // #region Private methods

    function _getMyDetails() {
        $http({
            method: "GET",
            url: "../../Person/GetMyDetails/"
        }).then(function mySucces(response) {
            $scope.me = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveMyDetails() {
        $http({
            method: "POST",
            url: "../../Person/UPDATE/",
            data: $scope.me
        }).then(function mySucces(response) {
            $scope.me = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _getTeam() {

        $http({
            method: "GET",
            url: "../../Team/Get/1"
        }).then(function mySucces(response) {
            $scope.team = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _myRemainingWork() {

        var remainingWork = 0;

        angular.forEach($scope.items, function (value, key) {
            remainingWork += parseFloat(value.workLeft);
        });

        $scope.myRemainingWork = remainingWork;
    }

    function _getItems() {

        $http({
            method: "GET",
            url: "../../Item/GetList/"
        }).then(function mySucces(response) {
            $scope.items = response.data;

            _myRemainingWork();

        }, function myError(response) {
            console.log(response.statusText);
        });

    }

    function _getStories() {

        $http({
            method: "GET",
            url: "../../Story/GetList/"
        }).then(function mySucces(response) {
            $scope.stories = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });

    }

    function _getItemDetails(itemId) {
        $http({
            method: "GET",
            url: "../../Item/Get/" + itemId
        }).then(function mySucces(response) {
            $scope.activeItem = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveItemDetails() {
        $http({
            method: "POST",
            url: "../../Item/Update/",
            data: $scope.activeItem
        }).then(function mySucces(response) {
          _getItems();
          _getStories();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveItem() {
        $http({
            method: "POST",
            url: "../../Item/Add/",
            data: $scope.activeItem
        }).then(function mySucces(response) {
          _getItems();
          _getStories();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _getStoryDetails(storyId) {
        $http({
            method: "GET",
            url: "../../Story/Get/" + storyId
        }).then(function mySucces(response) {
            $scope.activeStory = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveStoryDetails() {
        $http({
            method: "POST",
            url: "../../Story/Update/",
            data: $scope.activeStory
        }).then(function mySucces(response) {
            _getStories();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _getUser() {
        $http({
            method: "GET",
            url: "../../Person/GetMyDetails/"
        }).then(function mySucces(response) {
            $scope.user = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _getPersonDetails(personId) {
        $http({
            method: "GET",
            url: "../../Person/Get/" + personId
        }).then(function mySucces(response) {
            $scope.activeMember = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _savePersonDetails() {
        $http({
            method: "POST",
            url: "../../Person/Update/",
            data: $scope.activeMember
        }).then(function mySucces(response) {
            _getTeam();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _savePerson() {
        $http({
            method: "POST",
            url: "../../Person/Add/",
            data: $scope.activeMember
        }).then(function mySucces(response) {
            _getTeam();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _getProjects() {
        $http({
            method: "GET",
            url: "../../Project/GetList/"
        }).then(function mySucces(response) {
            $scope.projects = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _getProjectDetails(projectId) {
        $http({
            method: "GET",
            url: "../../Project/Get/" + projectId
        }).then(function mySucces(response) {
            $scope.activeProject = response.data;
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveProject() {
        $http({
            method: "POST",
            url: "../../Project/Add/",
            data: $scope.activeProject
        }).then(function mySucces(response) {
            _getProjects();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveProjectDetails() {
        $http({
            method: "POST",
            url: "../../Project/Update/",
            data: $scope.activeProject
        }).then(function mySucces(response) {
            _getProjects();
        }, function myError(response) {
            console.log(response.statusText);
        });
    }

    function _saveSprint() {
      $http({
        method: "POST",
        url: "../../Sprint/Add/",
        data: $scope.activeSprint
      }).then(function mySucces(response) {
        $scope.activeSprint = response.data;
        _getProjects();
      }, function myError(response) {
        console.log(response.statusText);
      });
    }
    
    function _saveSprintDetails() {
      $http({
        method: "POST",
        url: "../../Sprint/Update/",
        data: $scope.activeSprint
      }).then(function mySucces(response) {
        $scope.activeSprint = response.data;
        _getProjects();
      }, function myError(response) {
        console.log(response.statusText);
      });
    }
    
    function _getSprintDetails(sprintId) {
      $http({
        method: "GET",
        url: "../../Sprint/Get/" + sprintId
      }).then(function mySucces(response) {
        $scope.activeSprint = response.data;
      }, function myError(response) {
        console.log(response.statusText);
      });
    }

    // #endregion Private methods

    // #region Public methods

    $scope.getMyDetails = function () {
        _getMyDetails();
    };

    $scope.saveMyDetails = function () {
        _saveMyDetails();
    };

    $scope.getItemDetails = function (itemId) {
        
        $scope.editItem = true;
        $scope.newItem = false;

        // todo: find new way to handle events
        $("#task-modal").modal();

        _getItemDetails(itemId);
    };

    $scope.addNewItem = function (storyId) {

        $scope.editItem = false;
        $scope.newItem = true;

        $scope.activeItem = {
            storyId : storyId
        };

        // todo: find new way to handle events
        $("#task-modal").modal();
    };

    $scope.saveItemDetails = function () {
        _saveItemDetails();
    };

    $scope.addItem = function () {
        _saveItem();
    };

    $scope.getStoryDetails = function (storyId) {

        $scope.editStory = true;
        $scope.newStory = false;

        // todo: find new way to handle events
        $("#story-modal").modal();

        _getStoryDetails(storyId);
    };

    $scope.saveStoryDetails = function () {
        _saveStoryDetails();
    };

    $scope.getTeam = function () {
        _getTeam();

        $scope.viewMembersButton = false;
        $scope.hideMembersButton = true;
    };

    $scope.hideTeam = function () {
        $scope.viewMembersButton = true;
        $scope.hideMembersButton = false;
    };

    $scope.addNewPerson = function (teamId) {
        $scope.editMember = false;
        $scope.newMember = true;

        $scope.activeMember = {
            teamId: teamId
        };

        // todo: find new way to handle events
        $("#member-modal").modal();
    };

    $scope.saveMemberDetails = function () {
        _savePersonDetails();
    };

    $scope.addMember = function () {
        _savePerson();
    };

    $scope.getPersonDetails = function (personId) {
        $scope.editMember = true;
        $scope.newMember = false;

        _getPersonDetails(personId);

        // todo: find new way to handle events
        $("#member-modal").modal();
    };

    $scope.getProductBacklog = function () {
        _getStories();

        $scope.viewProductBacklogButton = false;
        $scope.hideProductBacklogButton = true;
    };

    $scope.hideProductBacklog = function () {
        $scope.viewProductBacklogButton = true;
        $scope.hideProductBacklogButton = false;
    };

    $scope.getProjects = function () {
        _getProjects();

        $scope.viewProjectsButton = false;
        $scope.hideProjectsButton = true;
    };

    $scope.hideProjects = function () {
        $scope.viewProjectsButton = true;
        $scope.hideProjectsButton = false;
    };

    $scope.getProjectDetails = function (projectId) {
        $scope.editProject = true;
        $scope.newProject = false;

        // todo: find new way to handle events
        $("#project-modal").modal();

        _getProjectDetails(projectId);
    };

    $scope.saveProjectDetails = function () {
        _saveProjectDetails();
    };

    $scope.addNewProject = function () {
        $scope.editProject = false;
        $scope.newProject = true;

        $scope.activeProject = {};

        // todo: find new way to handle events
        $("#project-modal").modal();
    };

    $scope.addProject = function () {
        _saveProject();
    };
      
    $scope.getSprintDetails = function (sprintId) {
      $scope.editSprint = true;

      // todo: find new way to handle events
      $("#sprint-modal").modal();

      _getSprintDetails(sprintId);
    };

    $scope.addNewSprint = function (projectId) {
      $scope.editSprint = false;
      $scope.activeSprint = {
        projectId: projectId
      };

      // todo: find new way to handle events
      $("#sprint-modal").modal();
    };

    $scope.addSprint = function () {
      _saveSprint();
    };

    $scope.saveSprintDetails = function () {
      _saveSprintDetails();
    };
    
    // #endregion Public methods

});