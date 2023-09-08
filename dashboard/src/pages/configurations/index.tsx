import { IResourceComponentsProps, useCreate } from "@refinedev/core";

import { DeleteButton, List, useModal, useTable } from "@refinedev/antd";

import {
  Button,
  Form,
  InputNumber,
  Modal,
  Select,
  Space,
  Table,
  Typography,
} from "antd";
import {
  BuildingsConfiguration,
  BuildingsConfigurationCreateDto,
} from "../../core/api";
import { buildingTypes } from "../../core/constants";
const { Title } = Typography;

export const BuildingsConfigurationList: React.FC<
  IResourceComponentsProps
> = () => {
  const { tableProps } = useTable<BuildingsConfiguration>();
  const { modalProps, show, close } = useModal();
  modalProps.width = 300;

  const CreateModal = () => {
    const { mutate: create } = useCreate();
    const [form] = Form.useForm<BuildingsConfigurationCreateDto>();
    const availableTypes = buildingTypes.filter(
      (x) => !tableProps.dataSource?.some((y) => x.value == y.type)
    );

    return (
      <Modal
        closeIcon={null}
        onOk={async () => {
          await form.submit();
        }}
        {...modalProps}
      >
        <Form<BuildingsConfigurationCreateDto>
          form={form}
          onFinish={async (values) => {
            await create({
              resource: "buildingsconfiguration",
              values,
            });

            close();
          }}
        >
          <Title level={4} style={{ textAlign: "center", paddingBottom: 8 }}>
            Create
          </Title>
          <Form.Item
            name="type"
            label="Building Type"
            rules={[{ required: true }]}
          >
            <Select options={availableTypes} style={{ maxWidth: 150 }} />
          </Form.Item>
          <Form.Item
            name="constructionTime"
            label="Building Time"
            rules={[{ required: true, min: 30, max: 1800, type: "number" }]}
          >
            <InputNumber size="large" style={{ width: 145 }} />
          </Form.Item>
          <Form.Item
            name="buildingCost"
            label="Building Cost"
            rules={[{ required: true, min: 0.1, type: "number" }]}
          >
            <InputNumber size="large" style={{ width: 145 }} />
          </Form.Item>
        </Form>
      </Modal>
    );
  };

  return (
    <>
      <List
        headerProps={{
          extra: <Button onClick={show}>Create</Button>,
        }}
      >
        <Table {...tableProps} rowKey="id">
          <Table.Column dataIndex="type" title="Building Type" />
          <Table.Column
            dataIndex="buildingCost"
            title="Building Cost"
            render={(value: string) => {
              return (
                <Typography.Paragraph
                  style={{
                    all: "revert",
                  }}
                >
                  {value} $
                </Typography.Paragraph>
              );
            }}
          />
          <Table.Column
            dataIndex="constructionTime"
            title="Building Time"
            render={(value: string) => {
              return (
                <Typography.Paragraph
                  style={{
                    all: "revert",
                  }}
                >
                  {value} Seconds
                </Typography.Paragraph>
              );
            }}
          />
          <Table.Column<BuildingsConfiguration>
            title="Delete"
            dataIndex={"actions"}
            render={(_text, record) => {
              return (
                <Space>
                  <DeleteButton
                    size="small"
                    recordItemId={record.id}
                    hideText
                  />
                </Space>
              );
            }}
          />
        </Table>
      </List>
      <CreateModal />
    </>
  );
};
