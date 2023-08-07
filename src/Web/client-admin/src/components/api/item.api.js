import axios from "axios";
import store from "../../store";

const BASE_URL = "http://localhost:5000";

const createItem = (payload) => {
    const formattedReq = {
        id: payload.id,
        itemCode: payload.itemCode,
        itemName: payload.itemName,
        skuCode: payload.skuCode,
        categoryId: payload.categoryId,
        displayInFrontPage: payload.displayInFrontPage,
        quantity: payload.quantity,
        price: payload.price,
        isShowWeb: payload.isShowWeb,
        shortDescription: payload.shortDescription,
        longDescription: payload.longDescription,
        itemImageUrl: payload.itemImageUrl,
        loyalityPoints: payload.loyalityPoints,
        longitude: payload.longitude,
        latitude: payload.latitude,
        categoryName: payload.categoryName
    };

    const config = {
        headers: {
            Authorization: `Bearer ${store.getState().auth.token}`
        }
    };
    let url = "/api/Product/saveProduct"

    return axios.post(BASE_URL + url, formattedReq, config)
        .then((response) => {
            return response;
        })
        .catch((error) => {
            throw new Error("Request failed: " + error.message);
        });
};

const getItems = (payload) => {
    const formattedReq = {
        searchText: payload.searchText,
        categoryId: payload.categoryId
      };

    const config = {
        headers: {
            Authorization: `Bearer ${store.getState().auth.token}`
        }
    };
    let url = "/api/Product/getProductsByFilter"

    return axios.post(BASE_URL + url, formattedReq, config)
        .then((response) => {
            return response;
        })
        .catch((error) => {
            throw new Error("Request failed: " + error.message);
        });
};

export { createItem, getItems };
