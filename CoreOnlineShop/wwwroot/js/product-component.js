Vue.component('product-manager', {
    template: `
        <div v-if="!editing">
                <button class="button" @click="newProduct">Add Product</button>
                <table class="table">
                    <tr>
                        <th>Id</th>
                        <th>Product</th>
                        <th>Value</th>
                    </tr>
                    <tr v-for="(product, index) in products">
                        <td>{{product.id}}</td>
                        <td>{{product.name}}</td>
                        <td>{{product.description}}</td>
                        <td><button class="button" @click="editProduct(product, index)">Edit</button></td>
                        <td><button class="button" @click="deleteProduct(product.id,index)">Remove</button></td>


                    </tr>
                </table>

            </div>

            <div v-else>
                <div class="field">
                    <label class="label">Name</label>
                    <div class="control">
                        <input class="input" v-model="productModel.name" />
                    </div>
                </div>

                <div class="field">
                    <label class="label">Description</label>
                    <div class="control">
                        <input class="input" v-model="productModel.description" />
                    </div>
                </div>

                <div class="field">
                    <label class="label">Value</label>
                    <div class="control">
                        <input class="input" v-model="productModel.value" />
                    </div>
                </div>
                <button class="button is-success" @click="createProduct" v-if="!productModel.id">Add New Product</button>
                <button class="button is-warning" @click="updateProduct" v-else>Update Product</button>
                <button class="button" @click="cancelProduct" v-if="editing">Cancel</button>

            </div>
            `,
    data() {
        return {
            editing: false,
            loading: false,
            objectIndex: 0,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product Description",
                value: 3.66
            },
            products: []
        };
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/product/' + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value
                    };
                })
                .catch(err => {
                    console.log(err);
                })
                .the(() => {
                    this.loading = false;
                });

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
                    this.loading = false;
                });

        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productModel)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                    this.loading = false;
                    this.editing = false;
                })
                .catch(err => {
                    console.log(err);
                })
                .the(() => {

                });
        },
        updateProduct() {
            this.loading = true;
            axios.put('/Admin/product', this.productModel)
                .then(res => {
                    console.log(res.data);
                    //this.products.push(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                    this.loading = false;
                    this.editing = false;
                })
                .catch(err => {
                    console.log(err);
                })
                .the(() => {

                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/Admin/product/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.log(err);
                })
                .the(() => {
                    this.loading = false;
                });

        },
        editProduct(product, index) {
            this.objectIndex = index;
            this.productModel = {
                id: product.id,
                name: product.name,
                description: product.description,
                value: product.value
            };
            this.editing = true;
        },
        cancelProduct() {
            this.editing = false;
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        }
    },
    computed: {

    }
})