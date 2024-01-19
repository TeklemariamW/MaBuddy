$(document).ready(function () {

    Vue.component('my-select', {
        props: ['optionss'],
        template: "#my-select",
        role: ''
    });


    var member = new Vue({

        el: '#member',

        data: {
            memberAndRole: {
                members: []
            },
            role: '',

            selected: '',
            options: [
                {text: 'Admin', value: 'Admin'},
                {text: 'Helper', value: 'Helper'},
                {text: 'Student', value: 'Student'}
            ]
        },
        created: function () {
            var self = this;
            // Fetch list of Questions
            axios.get("/api/MembersApi")
                .then(function (response) {

                    self.memberAndRole.members = response.data;
                });
        },
        methods: {

            handleAnswer: function (e) {
                var self = this;

                self.role = e.answer;
            },
            Edit: function (userId) {

                var self = this;

                // Find the corresponding user
                var currentUser = self.memberAndRole.members.find(function (m) {
                    return m.user.id === userId;
                });

                var selectedRole = currentUser.selectedRole;


                axios({
                    method: "post",
                    url: "/api/MembersApi/" + userId,
                    data: {
                        Role: selectedRole
                    }
                })
                    .then(function (response) {

                        axios.get("/api/MembersApi")
                            .then(function (response) {

                                self.memberAndRole.members = response.data;
                            });


                    });
            }
        }

    })
});