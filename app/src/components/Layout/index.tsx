import React, { FC } from 'react';
import { useToggle } from 'ahooks';
import { Avatar, Layout as AntLayout, Menu } from 'antd';
import {
  MenuUnfoldOutlined,
  MenuFoldOutlined,
  UserOutlined,
  VideoCameraOutlined,
} from '@ant-design/icons';
import { IProps } from './types';
import { useStyles } from './styles';
import { Typography } from 'antd';
import { AuthService } from '../../services';
import { useHistory } from 'react-router';
import { RouterPaths } from '../../consts';

const { Title, Text } = Typography;
const { Header, Sider, Content } = AntLayout;

const Layout: FC<IProps> = (props: IProps) => {
  const classes = useStyles();
  const history = useHistory();
  const user = AuthService.User;

  const [collapsed, { toggle }] = useToggle(false);
  
    return (
      <AntLayout className={classes.layoutContainer}>
        <Sider trigger={null} collapsible collapsed={collapsed}>
          <div className={classes.logo}>
          <Title level={3}>{!collapsed ? "Loquimini" : "L"}</Title>
          </div>
          <Menu theme="dark" mode="inline">
            <Menu.Item key="1" icon={<VideoCameraOutlined />}>
              Houses
            </Menu.Item>
            <Menu.Item key="2" icon={<UserOutlined />} onClick={() => history.push(RouterPaths.UserList)}>
              Users
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
            <div className={classes.profile}>
              <Avatar className={classes.user} icon={<UserOutlined />}/>
              <Text >{`${user.firstName} ${user.lastName}`.trim()}</Text>
              {/* <VideoCameraOutlined /> */}
            </div>
          </Header>
          <Content
            className={classes.content}
          >
            {props.children}
          </Content>
        </AntLayout>
      </AntLayout>
    );
};

export default Layout;