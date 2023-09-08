import { IResourceComponentsProps } from "@refinedev/core";

import { DeleteButton, List, useModal, useTable } from "@refinedev/antd";

import { Button, Modal, Space, Table, Typography } from "antd";
import { BuildingsConfiguration } from "../../core/api";

export const BuildingsConfigurationList: React.FC<
  IResourceComponentsProps
> = () => {
  const { tableProps } = useTable<BuildingsConfiguration>();
  const { modalProps, show, close } = useModal();
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
      <Modal onOk={close} {...modalProps}>
        Dummy Modal Content
      </Modal>
    </>
  );
};
