$(document).ready(function () {

    var faq = new Vue({

        el: '#faq',

        data: {
            Faqs: [],
            editFaq: null,

            faq: {
                FaqQuestion: '',
                FaqAnswer: '',
            }
        },
        created: function () {
            var self = this;

            // Fetch list of Questions
            axios.get("/api/FaqApi")
                .then(function (response) {

                    self.Faqs = response.data;
                });
        },
        methods: {
            addQuestion: function () {

                var self = this;

                if (self.faq.FaqQuestion.length === 0 || self.faq.FaqAnswer.length === 0)
                    return;

                axios.post('/api/FaqApi', self.faq).then(function (response) {
                    self.Faqs.push(response.data);

                    self.faq.FaqQuestion = '',
                        self.faq.FaqAnswer = ''

                })

            },

            remove: function (id) {
                var self = this;

                axios.delete('/api/FaqApi/' + id).then(function (response) {

                    self.Faqs = self.Faqs.filter(function (p) {
                        return p.id !== id
                    })

                })
            },
            Edit: function (id) {
                var self = this;


                axios({
                    method: 'put',
                    url: '/api/FaqApi/' + id,
                    data: {
                        id: id,
                        FaqQuestion: self.faq.FaqQuestion,
                        FaqAnswer: self.faq.FaqAnswer,
                    }
                })
                    .then(function (response) {
                        // handle success
                        self.faq.FaqQuestion = '',
                            self.faq.FaqAnswer = ''
                    })
                    .catch(function (error) {
                        // handle error
                        console.log(error);
                    })
                    .then(function () {
                        // Always execute
                        axios.get('/api/FaqApi')
                            .then(function (response) {
                                self.Faqs = response.data;
                            })
                    });
            },

        }

    })

});