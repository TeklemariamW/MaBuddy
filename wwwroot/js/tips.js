$(document).ready(function () {

    var tip = new Vue({

        el: '#tip',

        data: {
            Tips: [],
            editTip: null,

            tip: {
                TipTitle: '',
                TipText: '',
            }
        },
        created: function () {
            var self = this;

            // Fetch list of Questions
            axios.get("/api/TipsApi")
                .then(function (response) {

                    self.Tips = response.data;
                });
        },
        methods: {
            addTip: function () {

                var self = this;

                if (self.tip.TipTitle.length === 0 || self.tip.TipText.length === 0)
                    return;

                axios.post('/api/TipsApi', self.tip).then(function (response) {
                    self.Tips.push(response.data);

                    self.tip.TipTitle = '',
                        self.tip.TipText = ''

                })

            },

            remove: function (id) {
                var self = this;

                axios.delete('/api/TipsApi/' + id).then(function (response) {

                    self.Tips = self.Tips.filter(function (p) {
                        return p.id !== id
                    })

                })
            },
            Edit: function (id) {
                var self = this;


                axios({
                    method: 'put',
                    url: '/api/TipsApi/' + id,
                    data: {
                        id: id,
                        TipTitle: self.tip.TipTitle,
                        TipText: self.tip.TipText,
                    }
                })
                    .then(function (response) {
                        // handle success
                        self.tip.TipTitle = '',
                            self.tip.TipText = ''
                    })
                    .catch(function (error) {
                        // handle error
                        console.log(error);
                    })
                    .then(function () {
                        // Always execute
                        axios.get('/api/TipsApi')
                            .then(function (response) {
                                self.Tips = response.data;
                            })
                    });
            },

        }

    })

});