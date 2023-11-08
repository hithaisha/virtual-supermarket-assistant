import axios from 'axios'
import store from '../../store'

const BASE_URL = 'http://localhost:5000'
// const BASE_URL = 'http://morr.eastus.cloudapp.azure.com:8084'

const getUsers = (payload) => {
  const formattedReq = {
    searchText: payload.searchText,
  }

  const config = {
    headers: {
      Authorization: `Bearer ${store.getState().auth.token}`,
    },
  }
  let url = '/api/User/getAllUsersByFilter'

  return axios
    .post(BASE_URL + url, formattedReq, config)
    .then((response) => {
      return response
    })
    .catch((error) => {
      throw new Error('Request failed: ' + error.message)
    })
}

export { getUsers }
