var app = new Vue({
    el: '#app',
    data: {
        price: 0,
        showPrice: true,
        loading: false,
        products:[]
    },
    methods: {
        togglePrice: function () {
            this.showPrice = !this.showPrice;
        },
        alert: function (v) {
            alert(v);
        },
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .the(() => {
                    this.loading = true;
                });
            
        }
    },
    computed: {
        formatPrice: function () {
            return "$" + this.price;
        }
    }
})