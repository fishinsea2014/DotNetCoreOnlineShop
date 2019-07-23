var app = new Vue({
    el: '#app',

    data: {
        loading: false,
        products: [],
        selectedProduct: null,
        newStock: {
            productId: 0,
            description: 'Size',
            qty: 10
        }
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock(id) {
            this.loading = true;
            axios.get('/stocks/')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .finally(fnl => {
                    this.loading = false;
                });

        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete('/stocks/' + id)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .finally(fnl => {
                    this.loading = false;
                });
        },
        updateStock() {
            this.loading = true;
            axios.put('/stocks/',
                {
                    stock: this.selectedProduct.stock.map(x => {
                        return {
                            id: x.id,
                            description: x.description,
                            qty: x.qty,
                            productId: this.selectedProduct.id
                        };
                    })
                })
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .finally(fnl => {
                    this.loading = false;
                });
        },
        addStock() {
            this.loading = true;
            axios.post('/stocks/', this.newStock)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.push(res.data);
                    this.loading = false;
                })
                .catch(err => {
                    console.log(err);
                })
                .finally(fnl => {
                    this.loading = false;
                });
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;

        }
    }
});