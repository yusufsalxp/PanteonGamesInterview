import { useRegister } from "@refinedev/core";
import {
  Layout as AntdLayout,
  Button,
  Card,
  Col,
  Form,
  Input,
  Row,
  Typography,
} from "antd";
import React from "react";
import "./styles.css";

const { Text, Title } = Typography;

export interface IRegisterForm {
  username: string;
  email: string;
  password: string;
  passwordConfirm: string;
}

export const Register: React.FC = () => {
  const [form] = Form.useForm<IRegisterForm>();

  const { mutate: Register } = useRegister<IRegisterForm>();

  const CardTitle = (
    <Title level={3} className="title">
      Sign up
    </Title>
  );

  return (
    <AntdLayout className="layout">
      <Row
        justify="center"
        align="middle"
        style={{
          height: "100vh",
        }}
      >
        <Col xs={22}>
          <div className="container">
            <Card title={CardTitle} headStyle={{ borderBottom: 0 }}>
              <Form<IRegisterForm>
                layout="vertical"
                form={form}
                onFinish={(values) => {
                  Register(values);
                }}
                requiredMark={false}
                initialValues={{
                  username: "string",
                  email: "string@gmail.com",
                  password: "String2.",
                  passwordConfirm: "String2.",
                }}
              >
                <Form.Item
                  name="username"
                  label="Username"
                  rules={[{ required: true }]}
                >
                  <Input size="large" placeholder="Username" />
                </Form.Item>
                <Form.Item
                  name="email"
                  label="Email"
                  rules={[
                    { required: true },
                    {
                      type: "email",
                      message: "The input is not valid E-mail!",
                    },
                  ]}
                >
                  <Input size="large" placeholder="Email" />
                </Form.Item>
                <Form.Item
                  name="password"
                  label="Password"
                  rules={[{ required: true }]}
                >
                  <Input type="password" placeholder="●●●●●●●●" size="large" />
                </Form.Item>
                <Form.Item
                  name="passwordConfirm"
                  label="Password Confirm"
                  rules={[{ required: true }]}
                  style={{ marginBottom: "12px" }}
                >
                  <Input type="password" placeholder="●●●●●●●●" size="large" />
                </Form.Item>
                <Button type="primary" size="large" htmlType="submit" block>
                  Sign in
                </Button>
              </Form>
              <div style={{ marginTop: 8 }}>
                <Text style={{ fontSize: 12 }}>
                  have an account?{" "}
                  <a href="/login" style={{ fontWeight: "bold" }}>
                    Sign in
                  </a>
                </Text>
              </div>
            </Card>
          </div>
        </Col>
      </Row>
    </AntdLayout>
  );
};
