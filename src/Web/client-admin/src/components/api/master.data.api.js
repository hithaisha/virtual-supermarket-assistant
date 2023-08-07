import axios from "axios";
import store from "../../store";

const BASE_URL = "http://localhost:5000";

const getCategories = () => {
    const config = {
        headers: {
            Authorization: `Bearer ${store.getState().auth.token}`
        }
    };
    let url = "/api/MasterData/getCategoryMasterData"

    return axios.get(BASE_URL + url, config)
        .then((response) => {
            return response;
        })
        .catch((error) => {
            throw new Error("Request failed: " + error.message);
        });
};

export { getCategories };
