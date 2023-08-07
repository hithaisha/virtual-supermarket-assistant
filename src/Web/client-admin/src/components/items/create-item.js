import React, { Fragment } from "react";
import { Card, CardBody, CardHeader, Col, Container, Row } from "reactstrap";
import Breadcrumb from "../common/breadcrumb";
import TabsetItem from "./tabset-item";

const Create_item = () => {
	return (
		<Fragment>
			<Breadcrumb title="Create Item" parent="Items" />
			<Container fluid={true}>
				<Row>
					<Col sm="12">
						<Card>
							<CardHeader>
								<h5> Add Item</h5>
							</CardHeader>
							<CardBody>
								<TabsetItem />
							</CardBody>
						</Card>
					</Col>
				</Row>
			</Container>
		</Fragment>
	);
};

export default Create_item;
