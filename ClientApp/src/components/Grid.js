import React, { Component } from 'react'
import { Grid, Image, Card, CardContent, Icon, Divider } from 'semantic-ui-react';
import { NavLink } from 'react-router-dom';

export default class GridExampleRelaxed extends Component {
  constructor(props) {
    super(props);
    this.state = {
      items: [],
      images: [],
      isLoaded: false
    }
  }
  
  componentDidMount() {
    fetch('https://localhost:5001/api/product')
      .then(res => res.json())
      .then(json => {
        this.setState({
          isLoaded: true,
          items: json
        })
      });
    fetch('https://localhost:5001/api/image')
      .then(res => res.json())
      .then(json => {
        this.setState({
          isLoaded: true,
          images: json
        })
      });
  }
  render() {
    var { isLoaded, items, images } = this.state;
    if (!isLoaded) {
      return <div>Loading...</div>;
    }
    if (items && (items.length = 5)) {
      return (
        <Grid>
          <Grid.Row columns={5} centered relaxed>
            {items.map(item => (
              <Grid.Column>
                <NavLink to='./product'>
                  <Card href='#card-example-link-card' color='teal'>
                    {images.map(image => 
                      <Image src= {image.url} />
                    )}
                    <CardContent>
                      <Card.Header><Icon name='euro sign' />{item.price}</Card.Header>
                      <Card.Meta>{item.name}</Card.Meta>
                    </CardContent>
                  </Card>
                  <Divider hidden />
                </NavLink>
              </Grid.Column>
            ))}
          </Grid.Row>
        </Grid>
      )
    } else {
      return <div>No items found</div>
    }
  }
}