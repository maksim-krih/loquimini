import { FC } from 'react';
import { useToggle } from 'ahooks';
import { Layout as AntLayout, Menu } from 'antd';
import {
  MenuUnfoldOutlined,
  MenuFoldOutlined,
  UserOutlined,
  VideoCameraOutlined,
  UploadOutlined,
} from '@ant-design/icons';
import { IProps } from './types';
import { useStyles } from './styles';

const { Header, Sider, Content } = AntLayout;

const Layout: FC<IProps> = (props: IProps) => {
  const classes = useStyles();

  const [collapsed, { toggle }] = useToggle(false);
  
    return (
      <AntLayout>
        <Sider trigger={null} collapsible collapsed={collapsed}>
          <div className={classes.logo} />
          <Menu theme="dark" mode="inline" defaultSelectedKeys={['1']}>
            <Menu.Item key="1" icon={<UserOutlined />}>
              nav 1
            </Menu.Item>
            <Menu.Item key="2" icon={<VideoCameraOutlined />}>
              nav 2
            </Menu.Item>
            <Menu.Item key="3" icon={<UploadOutlined />}>
              nav 3
            </Menu.Item>
          </Menu>
        </Sider>
        <AntLayout className={classes.layout}>
          <Header className={classes.header}>
            {collapsed ? 
              <MenuUnfoldOutlined className={classes.trigger} onClick={() => toggle()}/> 
              : 
              <MenuFoldOutlined  className={classes.trigger} onClick={() => toggle()}/>
            }
          </Header>
          <Content
            className={classes.content}
          >
            Content
          </Content>
        </AntLayout>
      </AntLayout>
    );
};

export default Layout;