import React, { useState, useContext } from 'react'
import Link from 'next/link'
import CartContext from '../../../../helpers/cart'
import {
  Container,
  Row,
  Col,
  Media,
  Input,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  Card,
  CardImg,
  CardBody,
  CardTitle,
  CardText,
} from 'reactstrap'
import { CurrencyContext } from '../../../../helpers/Currency/CurrencyContext'
import cart from '../../../../public/assets/images/icon-empty-cart.png'
import { createOrder } from '../../../../api/order.api'
import QRCode from 'qrcode.react'
import { toast } from 'react-toastify'

const CartPage = () => {
  const context = useContext(CartContext)
  const cartItems = context.state
  const curContext = useContext(CurrencyContext)
  const symbol = curContext.state.symbol
  const total = context.cartTotal
  const removeFromCart = context.removeFromCart
  const [quantity, setQty] = useState(1)
  const [quantityError, setQuantityError] = useState(false)
  const updateQty = context.updateQty
  const [modalIsOpen, setModalIsOpen] = useState(false)
  const [qrData, setQrData] = useState(null)
  const auth = Boolean(JSON.parse(localStorage.getItem('authToken')))

  const handleQtyUpdate = (item, quantity) => {
    if (quantity >= 1) {
      setQuantityError(false)
      updateQty(item, quantity)
    } else {
      setQuantityError(true)
    }
  }
  console.log(cartItems)
  const saveOrder = async () => {
    if (!auth) {
      toast.info('You must login first')
    } else {
      try {
        const payload = {
          invoiceNumber: '',
          orderId: 0,
          totalPrice: cartItems?.reduce((total, item) => total + item.total, 0),
          orderItems: cartItems.map((item) => ({
            productId: item.id,
            quantity: Number(item.qty),
            totalPrice: item.total,
            itemName: item.itemName,
            itemImageUrl: item.itemImageUrl,
          })),
        }
        const response = await createOrder(payload)
        console.log(response.data)
        await setQrData(JSON.stringify(response.data))
        toast.success('Order placed successfully!')
        localStorage.removeItem('cartList')

        setTimeout(() => {
          toggleModal()
        }, 2000)
      } catch (e) {
        console.log(e)
        toast.error('Error try again')
      }
    }
  }

  const toggleModal = () => {
    setModalIsOpen(!modalIsOpen)
  }

  return (
    <div>
      <Modal isOpen={modalIsOpen} toggle={toggleModal} size="lg">
        <ModalHeader>QR Code</ModalHeader>
        <ModalBody style={{ padding: '10px' }}>
          <center>
            <h5>Scan the QR code and finish the order</h5>
            <QRCode value={qrData} size={300} className="mt-4 mb-3 m-3" />
          </center>
          <h4 className="">Order Details</h4>
          <Row>
            {JSON.parse(qrData)?.orderItems.map((item) => (
              <Col sm="4" key={item.productId}>
                <Card className="mb-3" style={{ boxShadow: 'inherit' }}>
                  <CardImg
                    top
                    width="100%"
                    src={item.itemImageUrl}
                    alt={item.itemName}
                  />
                  <CardBody>
                    <CardTitle>{item.itemName}</CardTitle>
                    <CardText>Quantity: {item.quantity}</CardText>
                    <CardText>Total Price: Rs.{item.totalPrice}</CardText>
                  </CardBody>
                </Card>
              </Col>
            ))}
          </Row>
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggleModal}>
            Close
          </Button>
        </ModalFooter>
      </Modal>

      {cartItems && cartItems.length > 0 ? (
        <section className="cart-section section-b-space">
          <Container>
            <Row>
              <Col sm="12">
                <table className="table cart-table table-responsive-xs">
                  <thead>
                    <tr className="table-head">
                      <th scope="col">image</th>
                      <th scope="col">product name</th>
                      <th scope="col">price</th>
                      <th scope="col">quantity</th>
                      <th scope="col">action</th>
                      <th scope="col">total</th>
                    </tr>
                  </thead>
                  {cartItems.map((item, index) => {
                    return (
                      <tbody key={index}>
                        <tr>
                          <td>
                            <Link href={`/product-details/` + item.id}>
                              <a>
                                <Media
                                  src={
                                    item.images
                                      ? item?.itemImageUrl
                                      : item?.itemImageUrl
                                  }
                                  alt=""
                                />
                              </a>
                            </Link>
                          </td>
                          <td>
                            <Link href={`/left-sidebar/product/` + item.id}>
                              <a>{item.itemName}</a>
                            </Link>
                            <div className="mobile-cart-content row">
                              <div className="col-xs-3">
                                <div className="qty-box">
                                  <div className="input-group">
                                    <input
                                      type="number"
                                      name="quantity"
                                      onChange={(e) =>
                                        handleQtyUpdate(item, e.target.value)
                                      }
                                      className="form-control input-number"
                                      defaultValue={Number(item.qty)}
                                      style={{
                                        borderColor: quantityError && 'red',
                                      }}
                                    />
                                  </div>
                                </div>
                                {item.qty >= item.stock ? 'out of Stock' : ''}
                              </div>
                              <div className="col-xs-3">
                                <h2 className="td-color">
                                  {symbol}
                                  {item.price}
                                </h2>
                              </div>
                              <div className="col-xs-3">
                                <h2 className="td-color">
                                  <a href="#" className="icon">
                                    <i
                                      className="fa fa-times"
                                      onClick={() => removeFromCart(item)}
                                    ></i>
                                  </a>
                                </h2>
                              </div>
                            </div>
                          </td>
                          <td>
                            <h2>
                              {symbol}
                              {parseFloat(item.price).toFixed(2)}
                            </h2>
                          </td>
                          <td>
                            <div className="qty-box">
                              <div className="input-group">
                                <input
                                  type="number"
                                  name="quantity"
                                  onChange={(e) =>
                                    handleQtyUpdate(item, e.target.value)
                                  }
                                  className="form-control input-number"
                                  defaultValue={item.qty}
                                  style={{
                                    borderColor: quantityError && 'red',
                                  }}
                                />
                              </div>
                            </div>
                            {item.qty >= item.stock ? 'out of Stock' : ''}
                          </td>
                          <td>
                            <i
                              className="fa fa-times"
                              onClick={() => removeFromCart(item)}
                            ></i>
                          </td>
                          <td>
                            <h2 className="td-color">
                              {symbol}
                              {parseFloat(item.total).toFixed(2)}
                            </h2>
                          </td>
                        </tr>
                      </tbody>
                    )
                  })}
                </table>
                <table className="table cart-table table-responsive-md">
                  <tfoot>
                    <tr>
                      <td>total price :</td>
                      <td>
                        <h2>
                          {symbol} {parseFloat(total).toFixed(2)}{' '}
                        </h2>
                      </td>
                    </tr>
                  </tfoot>
                </table>
              </Col>
            </Row>
            <Row className="cart-buttons">
              {/* <Col xs="6">
                <Link href={`/shop/left_sidebar`}>
                  <a className="btn btn-solid">continue shopping</a>
                </Link>
              </Col> */}
              <Col xs="12">
                <div onClick={saveOrder}>
                  <a className="btn btn-solid">Buy Now</a>
                </div>
              </Col>
            </Row>
          </Container>
        </section>
      ) : (
        <section className="cart-section section-b-space">
          <Container>
            <Row>
              <Col sm="12">
                <div>
                  <div className="col-sm-12 empty-cart-cls text-center">
                    <Media
                      src={cart}
                      className="img-fluid mb-4 mx-auto"
                      alt=""
                    />
                    <h3>
                      <strong>Your Cart is Empty</strong>
                    </h3>
                    <h4>Explore more shortlist some items.</h4>
                  </div>
                </div>
              </Col>
            </Row>
          </Container>
        </section>
      )}
    </div>
  )
}

export default CartPage
