$(document).ready(function () {

    Vue.component('question', {
        template:
            '<div>' +
            '<div class="container shadow p-3 mb-2 bg-warning rounded" style="height: auto;">' +
            '<h4 style="text-align: center;">Question {{ questionNumber }}</h4>' +
            '<h4 style="text-align: center;">{{question.questionText}} </h4>' +
            '<div class="container shadow p-3 mb-2 bg-white rounded form-check" style="height: auto;">' +
            '<img v-bind:src=question.formulaImage  style="margin: 0 auto; max-height: 200px; max-width: 400px">'+
            '</div>' +
            '</div>' +
            '<div>' +

            '<div class="container shadow p-3 mb-2 bg-white rounded form-check">' +
            '<input v-model="answer" type="radio" class="form-check-input" id="radio1" ' +
            'name="currentQuestion" :value="question.option1">' +
            '<label for="radio1"><h5>{{question.option1}}</h5></label>' +
            '</div>' +

            '<div class="container shadow p-3 mb-2 bg-white form-check rounded" >' +

            '<input v-model="answer" type="radio" class="form-check-input" id="radio2" ' +
            'name="currentQuestion" :value="question.option2">' +
            '<h5>{{question.option2}}</h5>' +
            '</div>' +

            '<div class="container shadow p-3 mb-2 bg-white rounded form-check">' +
            '<input v-model="answer" type="radio" class="form-check-input" id="radio3" ' +
            'name="currentQuestion" :value="question.option3">' +
            '<h5>{{question.option3}}</h5>' +
            '</div>' +

            '<div class="container shadow p-3 mb-2 bg-white rounded form-check">' +
            '<input v-model="answer" type="radio" class="form-check-input" id="radio4" ' +
            'name="currentQuestion" :value="question.option4">' +
            '<h5>{{question.option4}}</h5>' +
            '</div>' +

            '</div>' +
            '<button class="btn btn-block btn-success" v-on:click="submitAnswer"> Submit </button> <br>' +

            '</div>',

        data: function () {
            return {
                answer: ''
            }
        },
        props: ['question', 'question-number'],
        methods: {
            submitAnswer: function () {
                this.$emit('answer', {
                    answer: this.answer
                });
                this.answer = null;
            }
        }

    });

    var progress = new Vue({

        el: '#progress',

        data: {

            // introStage: false,
            questionStage: false,
            resultsStage: false,

            currentQuestion: 0,
            answers: [],
            correct: 0,
            percent: null,

            questions: [],
            title: ''
        },
        created: function () {
            var self = this;

            self.introStage = true;
        },

        methods: {

            DerivationQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "derivation";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.title = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            IntegralQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "integral";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.title = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },

            handleAnswer: function (e) {
                var self = this;

                self.answers[self.currentQuestion] = e.answer;
                if ((self.currentQuestion + 1) === self.questions.length) {
                    self.handleResults();
                    self.questionStage = false;
                    self.resultsStage = true;
                } else {
                    this.currentQuestion++;
                }
            },
            handleResults: function () {
                var self = this;

                self.questions.forEach(function (a, index) {
                    if (self.answers[index] === a.correctAnswer)
                        self.correct++;
                });

                self.percent = ((self.correct / self.questions.length) * 100).toFixed(2);
                self.currentQuestion = 0;
            },

            // Go to next question
            next: function () {
                this.currentQuestion++;
            },
            // Go to previous question
            prev: function () {
                this.currentQuestion--;
            },

            LimitQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "limit";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.title = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            ComplexNumberQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "complexNumber";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.search = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            SeriesQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "series";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.search = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            partialDerivationQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "partialDerivation";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.search = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            applicationOfDerivationQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "applicationOfDerivation";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.search = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            applicationOfIntegrationQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "applicationOfIntegration";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.search = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            inverseFunctionQuiz: function () {

                var self = this;

                self.questions = "";
                self.correct = 0;
                self.currentQuestion = 0;
                self.percent = null;

                self.title = "inverseFunction";

                // Fetch list of Questions
                axios.get("/api/ProgressesApi/" + self.title)
                    .then(function (response) {

                        self.questions = response.data;

                        // self.search = '';
                    });
                self.introStage = false;
                self.resultsStage = false;
                self.questionStage = true;
            },
            SubmitResult: function () {

                var self = this;

                axios({
                    method: "post",
                    url: "/api/ProgressesApi/" + self.title,
                    data: {
                        CountCorrect: progress.correct,
                        Percent: self.questions.length
                    }
                })

                    .then(function (response) {
                        window.location.assign("/")

                    });


            },
        }
    })

});