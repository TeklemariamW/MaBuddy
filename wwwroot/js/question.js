$(document).ready(function () {

    var question = new Vue({

        el: '#question',

        data: {
            Questions: [],
            editQuestion: null,
            search: '',

            question: {
                QuestionText: '',
                level: '',
                subject: '',

                option1: '',
                option2: '',
                option3: '',
                option4: '',
                correctAnswer: '',
                formulaimage: ''

            }
        },
        created: function () {
            var self = this;

            // Fetch list of Questions
            axios.get("/api/QuestionsApi")
                .then(function (response) {

                    self.Questions = response.data;
                });
        },
        methods: {
            addQuestion: function () {

                var self = this;

                if (self.question.QuestionText.length === 0 || self.question.option1.length === 0)
                    return;

                axios.post('/api/QuestionsApi', self.question).then(function (response) {
                    self.Questions.push(response.data);

                    self.question.QuestionText = '',

                        self.question.level = '',
                        self.question.Subject = '',

                        self.question.option1 = '',
                        self.question.option2 = '',
                        self.question.option3 = '',
                        self.question.option4 = '',
                        self.question.correctAnswer = '',
                        self.question.formulaimage = ''
                })

            },

            remove: function (id) {
                var self = this;

                axios.delete('/api/QuestionsApi/' + id).then(function (response) {

                    self.Questions = self.Questions.filter(function (p) {
                        return p.id !== id
                    })

                })
            },
            Edit: function (id) {
                var self = this;


                axios({
                    method: 'put',
                    url: '/api/QuestionsApi/' + id,
                    data: {
                        id: id,
                        QuestionText: self.question.QuestionText,
                        level: self.question.level,
                        subject: self.question.Subject,

                        option1: self.question.option1,
                        option2: self.question.option2,
                        option3: self.question.option3,
                        option4: self.question.option4,
                        correctAnswer: self.question.correctAnswer,
                        formulaImage: self.question.formulaimage

                    }
                })
                    .then(function (response) {
                        // handle success
                        self.question.QuestionText = '',
                            self.question.level = '',
                            self.question.subject = '',
                            self.question.option1 = '',
                            self.question.option2 = '',
                            self.question.option3 = '',
                            self.question.option4 = '',
                            self.question.correctAnswer = '',
                            self.question.formulaimage = ''
                    })
                    .catch(function (error) {
                        // handle error
                        console.log(error);
                    })
                    .then(function () {
                        // Always execute
                        axios.get('/api/QuestionsApi')
                            .then(function (response) {
                                self.Questions = response.data;
                            })
                    });
            },

            Search: function () {
                var self = this;

                // Fetch list of Questions
                axios.get("/api/QuestionsApi/" + self.search)
                    .then(function (response) {

                        self.Questions = response.data;

                        self.search = '';
                    });
            }

        }

    })

});